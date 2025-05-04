using UnityEngine;

[CreateAssetMenu(fileName = "new Turret Properties", menuName = "ScriptableObject/Turret Properties", order = 51)]
public class TurretProperties : ScriptableObject
{
    [Header("References")]
    public GameObject bulletPrefab;

    [Header("Attribute")]
    public int level;
    public float targetingRange;
    public float speedRotation;
    public float bulletPerSecond;
    public float offsetRotation;
}
