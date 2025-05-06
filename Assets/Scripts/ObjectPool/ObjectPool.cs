using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;

    protected Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject GetObjectFromPool()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity);
            newObj.SetActive(false);
            return newObj;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
