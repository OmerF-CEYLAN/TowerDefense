using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : MonoBehaviour
{
    [SerializeField] float fireRate, firePower, startCost, range;

    int tier;

    List<Enemy> targetsInRange = new List<Enemy>();

    Enemy target;

    float counter;

    public bool IsPlaced { get; set; }


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

    public float FirePower
    {
        get => firePower;
        set => firePower = value;
    }

    public float StartCost
    {
        get => startCost;
        set => startCost = value;
    }

    public float Range
    {
        get => range;
        set => range = value;
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
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));


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
                target.HitPoint -= firePower;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

       Gizmos.DrawWireSphere(transform.position, range);
    }


}
