using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private TurretProperties[] towers;

    private bool canBuild = false;
    private int selectedTower = 0;


    private void Awake()
    {
        main = this;
    }

    public TurretProperties GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int selectedTower)
    {
        canBuild = !canBuild;
        if (canBuild)
        {
            this.selectedTower = selectedTower;
            foreach (GameObject obj in LevelManager.main.plots)
            {
                obj.GetComponent<Plot>().AvailableForBuldingColor();
            }
        }
        else
        {
            foreach (GameObject obj in LevelManager.main.plots)
            {
                obj.GetComponent<Plot>().ResetColor();
            }
        }
    }

    public bool CanBuilding()
    {
        return canBuild;
    }
}
