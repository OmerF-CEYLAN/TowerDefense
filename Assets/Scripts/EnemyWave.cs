using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyAssets",menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{

    public List<EnemySpawnInfo> enemySpawnInfos;

    public float duration;

    public float spawnInterval;

    public int prizeMoney;

}
