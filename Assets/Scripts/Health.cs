using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int startHealth = 2;
    [SerializeField] private int cost = 10;

    private bool isDestroyed;
    private int health;

    private void Start()
    {
        Initialise();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0 && !isDestroyed)
        {
            MoneyManager.instance.AddMoney(cost);
            isDestroyed = true;
            GetComponent<EnemyController>().Destroy();
        }
    }

    public void Initialise()
    {
        health = startHealth;
        isDestroyed = false;
    }
}
