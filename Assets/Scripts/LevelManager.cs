using UnityEngine;
using System.Collections.Generic;

public class LevelManager: MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPosition;
    public Transform[] path;
    public List<GameObject> plots;

    private void Awake()
    {
        instance = this;
    }
}
