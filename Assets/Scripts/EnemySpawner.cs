using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemyWave waveToSpawn;

    [SerializeField] GameObject spawnPoint;

    float spawnTime = 0;

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

        foreach (var waveInfo in waveToSpawn.enemySpawnInfos)
        {
            for (int i = 0; i < waveInfo.enemyCount; i++)
            {
                Instantiate(waveInfo.enemyType, spawnPoint.transform.position,Quaternion.identity);

                yield return new WaitForSeconds(spawnTime);

            }
        }   

    }

}
