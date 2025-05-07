using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [System.Serializable]
    public class ObjectPoolData
    {
        public GameObject prefab;
        public int initialSize;
        [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();
    }

    [SerializeField] private List<ObjectPoolData> objectTypes = new List<ObjectPoolData>();
    private Dictionary<GameObject, ObjectPoolData> prefabToPool = new Dictionary<GameObject, ObjectPoolData>();

    protected void Start()
    {
        foreach (var poolData in objectTypes)
        {
            prefabToPool[poolData.prefab] = poolData;

            for (int i = 0; i < poolData.initialSize; i++)
            {
                GameObject obj = Instantiate(poolData.prefab);
                obj.SetActive(false);
                poolData.pool.Enqueue(obj);
            }
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (!prefabToPool.TryGetValue(prefab, out var poolData))
        {
            return Instantiate(prefab);
        }

        if (poolData.pool.Count == 0)
        {
            ExpandPool(poolData);
        }

        GameObject obj = poolData.pool.Dequeue();
        obj.SetActive(true);
        obj.GetComponent<PrefabIdentifier>().originalPrefab = prefab;
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj, GameObject originalPrefab)
    {
        if (!prefabToPool.TryGetValue(originalPrefab, out var poolData))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolData.pool.Enqueue(obj);
    }

    protected void ExpandPool(ObjectPoolData poolData, int expandBy = 3)
    {
        for (int i = 0; i < expandBy; i++)
        {
            GameObject obj = Instantiate(poolData.prefab);
            obj.SetActive(false);
            poolData.pool.Enqueue(obj);
        }
    }
}
