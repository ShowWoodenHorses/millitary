using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "ScriptableObject/Wave Setting")]
public class WaveSetting : ScriptableObject
{

    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public int count;
        public float delaySpawn;
    }

    [System.Serializable]
    public class WaveProperty
    {
        public float delaySpawnBetweenEnemy;
        public List<EnemyGroup> enemies = new List<EnemyGroup>();
    }

    public List<WaveProperty> waves = new List<WaveProperty>();
}


