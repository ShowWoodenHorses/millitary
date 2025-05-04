using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private GameObject upgradeeUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private GameObject[] upgrades;

    [Header("Attribute")]
    [SerializeField] private int baseUpgradeCost;
    [SerializeField] private float coeffUpgradeCost = 1.5f;

    private int level = 0;

    private void Awake()
    {
        SelectUpgrade(upgrades[level]);
    }

    private void Start()
    {
        upgradeBtn.onClick.AddListener(Upgrade);
        UpdateUpgradeCostText(baseUpgradeCost);
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
        if (baseUpgradeCost > LevelManager.main.money) return;

        int nextLevel = level + 1;

        if (nextLevel >= upgrades.Length) return;

        level = nextLevel;

        LevelManager.main.RemoveMoney(baseUpgradeCost);
        UpdateUpgradeCostText(CalculateCost());
        SelectUpgrade(upgrades[level]);
        baseUpgradeCost = CalculateCost();

        if (level == upgrades.Length - 1)
        {
            upgradeBtn.gameObject.SetActive(false);
            upgradeCostText.gameObject.SetActive(false);
        }
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level + 1, coeffUpgradeCost));
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

}
