using System;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public abstract Type PoolType { get; }

    public void ReturnToPool() => ObjectPoolManager.ReturnObject(this);
}
