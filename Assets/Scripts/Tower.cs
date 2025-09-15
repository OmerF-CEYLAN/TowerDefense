using UnityEngine;

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

    override protected void HitTarget()
    {
        counter += Time.deltaTime;

        if (target != null)
        {
            if (counter >= fireRate)
            {
                transform.LookAt(target.transform.position);

                towerAnimations.ShootAnim();

                audioSource.PlayOneShot(shotSound);

                counter = 0f;
                int dealedDamage = target.GetHit(firePower);

                MoneyManager.Instance.AddMoney(dealedDamage);

                totalDamage += dealedDamage;
            }

        }
    }


}
