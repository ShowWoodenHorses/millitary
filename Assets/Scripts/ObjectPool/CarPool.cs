using UnityEngine;

public class CarPool : MonoBehaviour
{
    public static CarPool instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
