using UnityEngine;

public class CarPool : ObjectPool
{
    public static CarPool instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
