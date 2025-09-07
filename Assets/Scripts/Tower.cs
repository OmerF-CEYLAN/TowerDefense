using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : MonoBehaviour
{
    [SerializeField] float fireRate, range;

    [SerializeField] int firePower;

    [SerializeField] int startCost;

    [SerializeField] int upgradeCost;

    int tier;

    List<Enemy> targetsInRange = new List<Enemy>();

    Enemy target;

    float counter;

    public bool IsPlaced { get; set; }

    [SerializeField] int totalDamage;

    #region Properties

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    public int Tier
    {
        get => tier;
        set => tier = value;
    }

    public int FirePower
    {
        get => firePower;
        set => firePower = value;
    }

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

    public float Range
    {
        get => range;
        set => range = value;
    }

    public int TotalDamage
    {
        get => totalDamage;
        set => totalDamage = value;
    }

    #endregion

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

    void DetectEnemyInRange()
    {
        Vector3 rangeCheckPos = transform.position;
        rangeCheckPos.y = 0;

        Collider[] enemyColliders = Physics.OverlapSphere(rangeCheckPos, range, LayerMask.GetMask("Enemy"));


        targetsInRange.RemoveAll(e => e == null || enemyColliders.Contains(e.GetComponent<Collider>()) == false);

        foreach (var col in enemyColliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null && targetsInRange.Contains(enemy) == false)
            {
                targetsInRange.Add(enemy);
            }
        }

        target = targetsInRange
                .OrderByDescending(e => e.currentPathIndex)
                .ThenBy(e => e.GetDistanceToNextPath())
                .FirstOrDefault();
       
    }

    void HitTarget()
    {
        counter += Time.deltaTime;

        if (target != null)
        {
            if(counter >= fireRate)
            {
                transform.LookAt(target.transform.position);

                counter = 0f;
                int dealedDamage = target.GetHit(firePower);

                MoneyManager.Instance.AddMoney(dealedDamage);

                totalDamage += dealedDamage;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 rangeCheckPos = transform.position;
        rangeCheckPos.y = 0;

        Gizmos.DrawWireSphere(rangeCheckPos, range);
    }


}
