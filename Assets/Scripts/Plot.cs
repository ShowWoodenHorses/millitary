using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color avaiableColor;
    [SerializeField] private Material invisiable;

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
        if(!BuildManager.main.CanBuilding()) renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (!BuildManager.main.CanBuilding()) renderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (turretObj != null)
        {
            turretUpgrade.OpenUpgradeUI();
            return;
        }

        if (!BuildManager.main.CanBuilding()) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.main.money)
        {
            return;
        }

        LevelManager.main.RemoveMoney(towerToBuild.cost);
        turretObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        renderer.material = invisiable;
        turretUpgrade = turretObj.GetComponent<UpgradeController>();
    }

    public void AvailableForBuldingColor()
    {
         renderer.material.color = avaiableColor;
    }

    public void ResetColor()
    {
        renderer.material.color = startColor;
    }
}
