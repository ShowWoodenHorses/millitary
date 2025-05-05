using UnityEngine;

[CreateAssetMenu(fileName = "new Turret Properties", menuName = "ScriptableObject/Turret Properties", order = 51)]
public class TurretProperties : ScriptableObject
{
    [Header("References")]
    public GameObject bulletPrefab;
    public GameObject prefab;

    [Header("Attribute")]
    public string towerName;
    public int cost;
    public int level;
    public float targetingRange;
    public float speedRotation;
    public float bulletPerSecond;
    public float offsetRotation;
    
    
    [Header("UI")]
    public Sprite icon;
    public string description;

 
}
