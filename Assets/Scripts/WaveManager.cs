using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int currentWaveIndex = 0;

    [SerializeField] List<EnemyWave> enemyWaves;

    [SerializeField] EnemySpawner enemySpawner;

    float counter = 0;

    private void Awake()
    {

    }

    void Start()
    {
        enemySpawner.StartEnemySpawn(enemyWaves[currentWaveIndex]);

        currentWaveIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveIndex >= enemyWaves.Count)
            return;

        counter += Time.deltaTime;

        if(counter > enemyWaves[currentWaveIndex - 1].duration)
        {
            enemySpawner.StartEnemySpawn(enemyWaves[currentWaveIndex]);

            currentWaveIndex++;

            counter = 0;
        }
    }
}
