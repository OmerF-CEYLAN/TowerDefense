using UnityEngine;

[CreateAssetMenu(fileName = "MYAssets",menuName = "UpgradeData")]

public class UpgradeData : ScriptableObject
{
    public float fireRate;
    public int firePower;
    public float range;
    public int upgradeCost;

}
