using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : TowerBase
{
    [SerializeField] int startCost, upgradeCost;

    int totalMoneySoent;

    public bool IsPlaced { get; set; }

    public int StartCost
    {
        get => startCost;
        set => startCost = value;
    }

    public int UpgradeCost
    {
        get => upgradeCost;
        set => upgradeCost = value;
    }

    public int TotalMoneySoent
    {
        get => totalMoneySoent;
        set => totalMoneySoent = value;
    }


    private void Start()
    {
        counter = fireRate;
        totalMoneySoent = startCost;
    }

    void Update()
    {
        if (!IsPlaced)
            return;

        if (gameObject.TryGetComponent(out VehicleSpawner vehicleSpawner))
            return;

        DetectEnemyInRange();
        HitTarget();
    }


}
