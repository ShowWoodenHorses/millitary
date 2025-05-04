using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button sellBtn;
    [SerializeField] private TextMeshProUGUI costSellText;

    [Header("Attribute")]
    [SerializeField] private int baseCost;

    private int costSell;

    public static event Action SellTurret;

    private void OnEnable()
    {
        UpgradeController.ChangeCost += SetCostSell;
    }

    private void OnDisable()
    {
        UpgradeController.ChangeCost -= SetCostSell;
    }

    private void Start()
    {
        sellBtn.onClick.AddListener(Sell);
        SetCostSell();
    }

    public void Sell()
    {
        Plot plot = transform.GetComponentInParent<Plot>();
        if(plot != null)
        {
            LevelManager.main.AddMoney(costSell);
            plot.SetDefaultState();
            Destroy(gameObject);
        }
    }

    private int UpdateCostSell()
    {
        return Mathf.RoundToInt(baseCost / 2);
    }

    public void UpdateCostSellText(int costSell)
    {
        costSellText.text = costSell.ToString();
    }

    private void SetCostSell()
    {
        costSell = UpdateCostSell();
        UpdateCostSellText(costSell);
    }

    public void UpdateSellCost(int cost)
    {
        baseCost = cost;
        SetCostSell();
    }
}
