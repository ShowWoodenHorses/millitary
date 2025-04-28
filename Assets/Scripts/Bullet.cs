using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Attribute")]
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 difference = (target.position - transform.position).normalized;
        rb.linearVelocity = difference * speedMove;

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
