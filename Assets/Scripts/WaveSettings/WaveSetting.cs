using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "Wave Setting", order = 52)]
public class WaveSetting : ScriptableObject
{
    [System.Serializable]
    public class WaveProperty
    {
        public float delayBetweenSpawnEnemy;
        public List<GameObject> enemiesForWave = new List<GameObject>();
        public List<int> countEveryEnemy = new List<int>();
    }

    public List<WaveProperty> waves = new List<WaveProperty>();
}
