using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{

    [SerializeField] List<UpgradeData> upgradeDatas;

    Tower thisTower;

    private void Start()
    {
        thisTower = GetComponent<Tower>();
    }

    public void UpgradeTower()
    {

        if (thisTower.Tier >= upgradeDatas.Count)
            return;

        UpgradeData upgradeData = upgradeDatas[thisTower.Tier];

        thisTower.Tier++;

        thisTower.FirePower = upgradeData.firePower;
        thisTower.FireRate = upgradeData.fireRate;
        thisTower.Range = upgradeData.range;

        thisTower.GetComponent<TowerUIController>().SetTowerUI();
    }


}
