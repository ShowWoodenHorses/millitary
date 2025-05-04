using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeeUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private GameObject[] upgrades;

    [Header("Attribute")]
    [SerializeField] private int upgradeCost = 100;
    [SerializeField] private float coeffUpgradeCost = 1.5f;

    private int level = 0;
    private int currentCost;
    private SellController sellController;

    public static event Action ChangeCost;

    private void Awake()
    {
        SelectUpgrade(upgrades[level]);
    }

    private void Start()
    {
        upgradeBtn.onClick.AddListener(Upgrade);
        UpdateUpgradeCostText(upgradeCost);
        sellController = GetComponent<SellController>();
        sellController.UpdateSellCost(currentCost);
    }

    public void OpenUpgradeUI()
    {
        upgradeeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeeUI.SetActive(false);
    }

    public void Upgrade()
    {
        if (upgradeCost > LevelManager.main.money) return;

        int nextLevel = level + 1;

        if (nextLevel >= upgrades.Length) return;

        level = nextLevel;

        LevelManager.main.RemoveMoney(upgradeCost);
        UpdateUpgradeCostText(CalculateCost());
        SelectUpgrade(upgrades[level]);
        currentCost += upgradeCost;
        sellController.UpdateSellCost(currentCost);
        upgradeCost = CalculateCost();

        if (level == upgrades.Length - 1)
        {
            upgradeBtn.gameObject.SetActive(false);
            upgradeCostText.gameObject.SetActive(false);
        }
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(level + 1, coeffUpgradeCost));
    }

    private void SelectUpgrade(GameObject upgrade)
    {
        foreach (GameObject obj  in upgrades)
        {
            obj.SetActive(false);
        }
        upgrade.SetActive(true);
    }

    private void UpdateUpgradeCostText(int cost)
    {
        upgradeCostText.text = cost.ToString();
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void SetCurrentCost(int cost)
    {
        currentCost = cost;
    }

}
