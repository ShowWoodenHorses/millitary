using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static WaveSetting;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WaveSetting waveSetting;

    [Header("Attributes")]
    [SerializeField] private float timeBetweenWave = 5f;
    [SerializeField] private int maxWave = 10;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 0;
    private float enemiesAlive;
    private float enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    void Start()
    {
        maxWave = waveSetting.waves.Count;
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        WaveProperty wave = waveSetting.waves[currentWave];

        yield return new WaitForSeconds(timeBetweenWave);
        isSpawning = true;
        enemiesLeftToSpawn = EnemyPerWave();

        for (int i = 0; i < wave.enemiesForWave.Count; i++)
        {
            for (int j = 0; j < wave.countEveryEnemy[i];)
            {
                yield return new WaitForSeconds(wave.delayBetweenSpawnEnemy);
                enemiesLeftToSpawn--;
                enemiesAlive++;

                Instantiate(wave.enemiesForWave[i], LevelManager.main.startPosition.position, Quaternion.identity);

                j++;
            }
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        currentWave++;
        if(currentWave >= maxWave)
        {
            currentWave = maxWave - 1;
        }
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private int EnemyPerWave()
    {
        int allEnemy = 0;
        WaveProperty wave = waveSetting.waves[currentWave];
        for (int i = 0; i < wave.countEveryEnemy.Count; i++)
        {
            allEnemy += wave.countEveryEnemy[i];
        }

        return allEnemy;
    }
}
