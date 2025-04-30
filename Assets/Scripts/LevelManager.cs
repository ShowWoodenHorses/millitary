using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPosition;
    public Transform[] path;

    public int money;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        money = 100;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        if(amount <= money)
        {
            money -= amount;
        }
        else
        {
            // no money
        }
    }
}
