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
            MoneyManager.instance.AddMoney(costSell);
            plot.SetDefaultState();
            Destroy(gameObject);
        }
    }

    private int CalculateCostSell()
    {
        return Mathf.RoundToInt(baseCost / 2);
    }

    public void UpdateCostSellText(int costSell)
    {
        costSellText.text = costSell.ToString();
    }

    private void SetCostSell()
    {
        costSell = CalculateCostSell();
        UpdateCostSellText(costSell);
    }

    public void UpdateSellCost(int cost)
    {
        baseCost = cost;
        SetCostSell();
    }
}
