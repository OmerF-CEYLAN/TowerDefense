using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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

        if (MoneyManager.Instance.HaveEnoughMoney(upgradeData.upgradeCost) == false)
            return;

        MoneyManager.Instance.SpendMoney(upgradeData.upgradeCost);
        thisTower.FirePower = upgradeData.firePower;
        thisTower.FireRate = upgradeData.fireRate;
        thisTower.Range = upgradeData.range;

        if(upgradeData is VehicleUpgradeData)
        {
            if(thisTower.TryGetComponent(out VehicleSpawner vehicleSpawner))
            {
                VehicleUpgradeData vehicleUpgradeData = upgradeData as VehicleUpgradeData;
                vehicleSpawner.SetVehicleData(vehicleUpgradeData);
            }

        }

        thisTower.Tier++;

        if (thisTower.Tier < upgradeDatas.Count)
        {
            UpgradeData nextUpgradeData = upgradeDatas[thisTower.Tier];

            thisTower.UpgradeCost = nextUpgradeData.upgradeCost;
        }
        else
        {
            thisTower.UpgradeCost = 0; //if its zero then disable it on Set Tower UI
        }

        thisTower.GetComponent<TowerUIController>().SetTowerUI();
    }


}
