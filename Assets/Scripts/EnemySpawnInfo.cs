using UnityEngine;

[CreateAssetMenu(fileName = "MyAssets",menuName = "EnemySoawnInfo")]

public class EnemySpawnInfo : ScriptableObject
{

    public GameObject enemyType;
    public int enemyCount;

}
