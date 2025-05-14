using UnityEngine;

public class TurretShoot : TurretController, IAttackable
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletStartPosition;

    private Transform target;

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowarsTarget();

        if (!TargetIsInRange() || !target.gameObject.activeInHierarchy)
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / attackPerSecond)
            {
                Attack();
                timeUntilFire = 0f;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            turretRotationPoint.position,
            targetingRange,
            turretRotationPoint.forward,
            targetingRange,
            targetLayer
        );

        if (hits.Length > 0)
        {
            if (hits.Length == 1)
            {
                target = hits[0].transform;
                return;
            }

            target = FindTargetNearerFinish(hits);

        }
    }

    private void RotateTowarsTarget()
    {
        Vector3 difference = turretRotationPoint.position - target.position;
        difference.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(difference) * Quaternion.Euler(0f, offsetRotation, 0f);
        turretRotationPoint.rotation = Quaternion.Slerp(
            turretRotationPoint.rotation,
            targetRotation,
            speedRotation * Time.deltaTime
        );
    }

    public void Attack()
    {
        GameObject bullet = BulletPool.instance.GetObjectFromPool(bulletPrefab);
        bullet.transform.position = bulletStartPosition.position;
        bullet.transform.rotation = bulletStartPosition.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }

    private bool TargetIsInRange()
    {
        return Vector3.Distance(turretRotationPoint.position, target.position) < targetingRange;
    }

    private Transform FindTargetNearerFinish(RaycastHit[] hits)
    {
        int min = hits[0].transform.gameObject.GetComponent<EnemyController>().GetCountPointToFinish();
        Transform target = hits[0].transform;

        for (int i = 0; i < hits.Length; i++)
        {
            int countPointToFinish = hits[i].transform.gameObject.GetComponent<EnemyController>().GetCountPointToFinish();
            if (countPointToFinish < min)
            {
                min = countPointToFinish;
                target = hits[i].transform;
            }
        }

        return target;
    }

}
