using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private bool canBuild = false;
    private int selectedTower = 0;


    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int selectedTower)
    {
        canBuild = !canBuild;
        if (canBuild)
        {
            this.selectedTower = selectedTower;
            foreach (GameObject obj in LevelManager.instance.plots)
            {
                obj.GetComponent<Plot>().AvailableForBuldingColor();
            }
        }
        else
        {
            foreach (GameObject obj in LevelManager.instance.plots)
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
