using UnityEngine;

public class BulletPool : ObjectPool
{
    public static BulletPool instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
}
