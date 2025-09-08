using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : TowerBase
{
    [SerializeField] int startCost, upgradeCost;

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


    private void Start()
    {
        counter = fireRate;
    }

    void Update()
    {
        if (!IsPlaced)
            return;

        DetectEnemyInRange();
        HitTarget();
    }


}
