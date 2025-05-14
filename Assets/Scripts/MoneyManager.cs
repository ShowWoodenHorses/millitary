using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private int startMoney;

    public static MoneyManager instance;

    public int money;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = startMoney;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
        }
    }
}
