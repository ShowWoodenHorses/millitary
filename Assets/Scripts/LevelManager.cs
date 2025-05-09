using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPosition;
    public Transform[] path;
    public GameObject[] plots;

    public int money;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = 2000;
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
    }
}
