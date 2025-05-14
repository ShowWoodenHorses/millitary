using System.Collections;
using UnityEngine;

public class TurretFreeze : TurretController, IAttackable
{
    [Header("Attribute")]
    [SerializeField] private float freezeTime = 2f;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackPerSecond)
        {
            Attack();
            timeUntilFire = 0f;
        }
    }

    public void Attack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            targetingRange,
            transform.forward,
            targetingRange,
            targetLayer
        );

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                EnemyController em = hit.transform.GetComponent<EnemyController>();
                em.UpdateSpeedMove(em.GetSpeedMove() / 2);
                StartCoroutine(ResetFreeze(em));
            };
        }
    }

    private IEnumerator ResetFreeze(EnemyController em)
    {
        yield return new WaitForSeconds(freezeTime);
        em.ResetSpeedMove();
    }

}
