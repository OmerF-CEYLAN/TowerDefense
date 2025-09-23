using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemyWave waveToSpawn;

    [SerializeField] GameObject spawnPoint;

    float spawnTime = 0;

    bool isSpawning;

    public bool IsSpawning
    {
        get => isSpawning;
        set => isSpawning = value;
    } 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartEnemySpawn(EnemyWave wave)
    {
        waveToSpawn = wave;

        spawnTime = wave.spawnInterval;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        foreach (var waveInfo in waveToSpawn.enemySpawnInfos)
        {
            for (int i = 0; i < waveInfo.enemyCount; i++)
            {
                Instantiate(waveInfo.enemyType, spawnPoint.transform.position,Quaternion.identity);

                yield return new WaitForSeconds(spawnTime);

            }
        }

        isSpawning = false;

    }

}
