using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    public static EnemyPool instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}