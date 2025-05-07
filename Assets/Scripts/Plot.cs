using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color avaiableColor;
    [SerializeField] private Material invisiableMaterial;

    private GameObject turretObj;
    private UpgradeController turretUpgrade;
    private Color startColor;
    private Material defaultMaterial;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
        defaultMaterial = renderer.material;
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

        if(towerToBuild.cost > LevelManager.instance.money)
        {
            return;
        }

        LevelManager.instance.RemoveMoney(towerToBuild.cost);
        turretObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turretObj.transform.parent = transform;
        renderer.material = invisiableMaterial;
        turretUpgrade = turretObj.GetComponent<UpgradeController>();
        turretUpgrade.SetCurrentCost(towerToBuild.cost);
    }

    public void AvailableForBuldingColor()
    {
         renderer.material.color = avaiableColor;
    }

    public void ResetColor()
    {
        renderer.material.color = startColor;
    }

    public void SetDefaultState()
    {
        turretObj = null;
        renderer.material = defaultMaterial;
        renderer.material.color = startColor;
    }
}
