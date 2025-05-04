using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 2;
    [SerializeField] private int cost = 10;

    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.AddMoney(cost);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
