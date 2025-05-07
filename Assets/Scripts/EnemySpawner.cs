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

        foreach(var enemy in wave.enemies)
        {
            yield return new WaitForSeconds(wave.delaySpawnBetweenEnemy);

            for (int i = 0; i < enemy.count; i++)
            {
                yield return new WaitForSeconds(enemy.delaySpawn);

                enemiesLeftToSpawn--;
                enemiesAlive++;

                SpawnEnemy(enemy.enemyPrefab);
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
        
        foreach(var enemy in wave.enemies)
        {
            allEnemy += enemy.count;
        }

        return allEnemy;
    }

    private void SpawnEnemy(GameObject obj)
    {
        GameObject enemy = EnemyPool.instance.GetObjectFromPool(obj);
        enemy.GetComponent<EnemyController>().Initialise();
        enemy.SetActive(true);

    }
}
