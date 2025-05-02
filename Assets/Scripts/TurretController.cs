using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletStartPosition;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float speedRotation = 100f;
    [SerializeField] private float offsetRotation = -90f;
    [SerializeField] private float bulletPerSecond = 2f;

    private Transform target;
    private float timeUntilFire;

    void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowarsTarget();

        if (!TargetIsInRange())
        {
            target = null;
        } 
        else
        {
            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f / bulletPerSecond)
            {
                Shoot();
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
            target = hits[0].transform;
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

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }

    private bool TargetIsInRange()
    {
        return Vector3.Distance(turretRotationPoint.position, target.position) < targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(turretRotationPoint.position, targetingRange);

    }
}
