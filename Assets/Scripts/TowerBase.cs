using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{

    [SerializeField] protected float fireRate, range;
    [SerializeField] protected int firePower,totalDamage;
    protected int tier;

    protected List<Enemy> targetsInRange = new List<Enemy>();

    protected Enemy target;

    protected float counter;

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

    protected virtual void DetectEnemyInRange()
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

    protected virtual void HitTarget() 
    {
        counter += Time.deltaTime;

        if (target != null)
        {
            if (counter >= fireRate)
            {
                transform.LookAt(target.transform.position);

                counter = 0f;
                int dealedDamage = target.GetHit(firePower);

                MoneyManager.Instance.AddMoney(dealedDamage);

                totalDamage += dealedDamage;
            }

        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 rangeCheckPos = transform.position;
        rangeCheckPos.y = 0;

        Gizmos.DrawWireSphere(rangeCheckPos, range);
    }

}
