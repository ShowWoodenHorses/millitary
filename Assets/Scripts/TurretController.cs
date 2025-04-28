using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask targetLayer;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float speedRotation = 100f;
    [SerializeField] private float offsetRotation = -90f;

    private Transform target;
    private Quaternion startPotation;

    private void Start()
    {
        startPotation = turretRotationPoint.rotation;
    }

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

    private void ReturnDefaultRotation()
    {
        turretRotationPoint.rotation = Quaternion.Lerp(turretRotationPoint.rotation, startPotation, speedRotation);
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
