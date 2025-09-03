using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : MonoBehaviour
{
    [SerializeField] float fireRate, firePower, startCost, range;

    List<Enemy> targetsInRange = new List<Enemy>();

    Enemy target;

    float counter;

    public bool IsPlaced { get; set; }

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
                target.SetHitPointUI();
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        // Draw the sphere
        //Gizmos.DrawSphere(transform.position, range);

        // Draw wire sphere outline

       Gizmos.DrawWireSphere(transform.position, range);
    }


}
