using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherZombieSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime;

    [SerializeField] GameObject spawnPoint;

    [SerializeField] List<GameObject> enemiesToSpawn;

    Enemy thisEnemy;

    void Start()
    {
        thisEnemy = GetComponent<Enemy>();

        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject selectedEnemyObj = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];

            GameObject spawnedEnemyObj = Instantiate(selectedEnemyObj, spawnPoint.transform.position, Quaternion.identity);

            if(thisEnemy.GetDistanceToNextPath() <= Vector3.Distance(transform.position, spawnPoint.transform.position))
            {
                spawnedEnemyObj.GetComponent<Enemy>().currentPathIndex = thisEnemy.currentPathIndex < PathHolder.Instance.pathPoints.Count - 1
                ? thisEnemy.currentPathIndex + 1
                : thisEnemy.currentPathIndex;
            }
            else
            {
                spawnedEnemyObj.GetComponent<Enemy>().currentPathIndex = thisEnemy.currentPathIndex;
            }

            yield return new WaitForSeconds(spawnTime);
        }

    }

}
