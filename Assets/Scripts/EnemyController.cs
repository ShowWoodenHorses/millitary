using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PrefabIdentifier))]
public class EnemyController : MonoBehaviour
{

    [SerializeField] private float speedMove;
    [SerializeField] private float speedRotate;
    [SerializeField] private float offset = -90f;
    [SerializeField] private float minDisatnce = 0.1f;

    private Transform target;
    private Rigidbody rb;
    private int indexPath;
    private float baseSpeedMove;
    private int countPointToFinish;

    public void Initialise()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = LevelManager.instance.startPosition.position;
        indexPath = 0;
        target = LevelManager.instance.path[indexPath];
        countPointToFinish = LevelManager.instance.path.Length;
        baseSpeedMove = speedMove;
        Rotate();
    }

    private void Update()
    {
        Rotate();
        if (Vector3.Distance(target.position, transform.position) <= minDisatnce)
        {
            indexPath++;
            countPointToFinish--;

            if (indexPath >= LevelManager.instance.path.Length)
            {
                Destroy();
                return;
            }
            else
            {
                target = LevelManager.instance.path[indexPath];
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

    public void UpdateSpeedMove(float speed)
    {
        speedMove = speed;
    }

    public void ResetSpeedMove()
    {
        speedMove = baseSpeedMove;
    }

    public float GetSpeedMove()
    {
        return speedMove;
    }

    public int GetCountPointToFinish()
    {
        return countPointToFinish;
    }

    public void Destroy()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        EnemyPool.instance.ReturnObjectToPool(gameObject, GetComponent<PrefabIdentifier>().originalPrefab);
        GetComponent<Health>().Initialise();
        transform.position = Vector3.zero;
    }
}
