using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected LayerMask targetLayer;

    [Header("Attribute")]
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float speedRotation = 100f;
    [SerializeField] protected float offsetRotation = -90f;
    [SerializeField] protected float attackPerSecond = 2f;

    protected float timeUntilFire;

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

    }
}
