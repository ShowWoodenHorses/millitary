using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 2;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("enemy healsth: " + health);

        if(health <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
