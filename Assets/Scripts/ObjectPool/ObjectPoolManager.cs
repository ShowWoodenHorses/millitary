using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static Dictionary<Type, Queue<GameObject>> pools = new Dictionary<Type, Queue<GameObject>>();

    public void CreatePool(PoolableObject prefab, int size)
    {
        Type poolType = prefab.PoolType;
        if (!pools.ContainsKey(poolType))
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                var obj = Instantiate(prefab.gameObject);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            pools.Add(poolType, queue);
        }
    }

    public GameObject GetObject(PoolableObject prefab)
    {
        Type poolType = prefab.PoolType;
        if (pools.TryGetValue(poolType, out var queue) && queue.Count > 0)
        {
            return queue.Dequeue();
        }
        return Instantiate(prefab.gameObject);
    }

    public static void ReturnObject(PoolableObject obj)
    {
        Type poolType = obj.PoolType;
        if (!pools.ContainsKey(poolType))
        {
            pools[poolType] = new Queue<GameObject>();
        }

        obj.gameObject.SetActive(false);
        pools[poolType].Enqueue(obj.gameObject);
    }
}
