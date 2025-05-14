using UnityEngine;
using System.Collections.Generic;

public class SpawnPlots : MonoBehaviour
{
    [SerializeField] private GameObject prefabPlot;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform position in spawnPoints)
        {
            GameObject obj = Instantiate(prefabPlot, position.position, Quaternion.identity);
            obj.transform.parent = transform;
            LevelManager.instance.plots.Add(obj);
        }
    }

    public void Initialize()
    {
        //
    }
}
