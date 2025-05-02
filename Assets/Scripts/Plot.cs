using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color hoverColor;

    private GameObject turretObj;
    private UpgradeController turretUpgrade;
    private Color startColor;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    void Start()
    {
        startColor = renderer.material.color;
    }

    private void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (turretObj != null)
        {
            turretUpgrade.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.main.money)
        {
            return;
        }

        LevelManager.main.RemoveMoney(towerToBuild.cost);
        turretObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turretUpgrade = turretObj.GetComponent<UpgradeController>();
    }
}
