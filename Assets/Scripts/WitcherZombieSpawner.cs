using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherZombieSpawner : MonoBehaviour
{
    float spawnTime;

    [SerializeField] GameObject spawnPoint;

    [SerializeField] List<GameObject> enemiesToSpawn;

    [SerializeField] GameObject spawnEffect;

    Enemy thisEnemy;

    void Start()
    {
        spawnEffect.SetActive(false);

        spawnTime = Random.Range(5f, 10f);

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
            spawnTime = Random.Range(5f, 10f);

            GameObject selectedEnemyObj = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];

            GameObject spawnedEnemyObj = Instantiate(selectedEnemyObj, spawnPoint.transform.position, Quaternion.identity);

            spawnEffect.SetActive(true);

            yield return new WaitForSeconds(1f);

            spawnEffect.SetActive(false);

            if (thisEnemy.GetDistanceToNextPath() <= Vector3.Distance(transform.position, spawnPoint.transform.position))
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
