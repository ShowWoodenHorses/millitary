using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 2;

    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("enemy healsth: " + health);

        if(health <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
