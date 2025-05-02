using System.Collections;
using UnityEngine;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask targetLayer;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float attackPerSecond = 0.25f;
    [SerializeField] private float freezeTime = 2f;

    
    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackPerSecond)
        {
            Freeze();
            timeUntilFire = 0f;
        }
    }

    private void Freeze()
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

    }
}
