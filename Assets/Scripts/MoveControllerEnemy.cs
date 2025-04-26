using System.Collections.Generic;
using UnityEngine;

public class MoveControllerEnemy : MonoBehaviour
{

    [SerializeField] private float speedMove;

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
    }

    private void Update()
    {
        if(Vector3.Distance(target.position, transform.position) <= 0.1f)
        {
            indexPath++;

            if(indexPath >= LevelManager.main.path.Length)
            {
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
}
