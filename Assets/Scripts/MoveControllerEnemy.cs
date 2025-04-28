using System.Collections.Generic;
using UnityEngine;

public class MoveControllerEnemy : MonoBehaviour
{

    [SerializeField] private float speedMove;
    [SerializeField] private float speedRotate;
    [SerializeField] private float offset = -90f;
    [SerializeField] private float minDisatnce = 0.1f;

    private int indexPath = 0;
    private Transform target;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        target = LevelManager.main.path[indexPath];
        Rotate();
    }

    private void Update()
    {
        Rotate();
        if (Vector3.Distance(target.position, transform.position) <= minDisatnce)
        {
            indexPath++;

            if(indexPath >= LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[indexPath];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * speedMove;
    }

    private void Rotate()
    {
        Vector3 difference = target.position - transform.position;
        difference.y = 0;
        difference.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(difference) * Quaternion.Euler(0f, offset, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotate * Time.deltaTime);
    }
}
