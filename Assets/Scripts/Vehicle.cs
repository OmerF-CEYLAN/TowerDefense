using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : TowerBase
{
    [SerializeField] float speed;
    [SerializeField] int health;
    int maxHealth;

    float barChangeSpeed = 1f;

    [SerializeField] GameObject healthPanel;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image healthBar;

    [SerializeField] int segments = 60;
    [SerializeField] float lineWidth = 0.05f;
    [SerializeField] LineRenderer lr;

    int currentPathIndex;

    Rigidbody rb;

    public Tower militaryBaseTower;

    public float Speed
    {
        get => speed; set => speed = value;
    }

    public int Health
    {
        get => health; set => health = value;
    }

    void Start()
    {
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        currentPathIndex = PathHolder.Instance.pathPoints.Count - 1;

        maxHealth = health;

        counter = fireRate;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        DetectEnemyInRange();
        HitTarget();
        Move();

        if (healthPanel.activeSelf)
        {
            HealthBarLookAtPlayer();
            SetHealthUI();
        }

    }

    void Move()
    {
        if (currentPathIndex < 0)
            return;

        Transform target = PathHolder.Instance.pathPoints[currentPathIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= speed * Time.deltaTime)
        {
            currentPathIndex--;

            if (currentPathIndex >= 0)
            {
                transform.LookAt(PathHolder.Instance.pathPoints[currentPathIndex].position);
            }
        }

    }

    protected override void HitTarget()
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

                militaryBaseTower.TotalDamage += dealedDamage;
            }

        }
    }

    public void CrashToEnemies(Enemy crashedEnemy)
    {
        int dealedDamage = health >= crashedEnemy.HitPoint ? crashedEnemy.HitPoint : health;

        health -= dealedDamage;

        militaryBaseTower.TotalDamage += dealedDamage;

        crashedEnemy.GetHit(dealedDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }

        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            CrashToEnemies(enemy);
        }
    }

    public void DrawRangeCircle(float radius)
    {
        lr.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / segments) * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

    public void SetHealthUI()
    {
        DrawRangeCircle(range);

        healthText.text = health + " / " + maxHealth;

        DOTween.To(() => healthBar.fillAmount, x => healthBar.fillAmount = x, (float)health / maxHealth, barChangeSpeed);
    }

    void HealthBarLookAtPlayer()
    {
        healthPanel.transform.LookAt(Camera.main.transform.position);

        healthPanel.transform.Rotate(0, 180, 0);
    }

    public void EnableHitPointUI()
    {
        lr.gameObject.SetActive(true);
        healthPanel.SetActive(true);
        SetHealthUI();
    }

    public void DisableHitPointUI()
    {
        lr.gameObject.SetActive(false);
        healthPanel.SetActive(false);
    }



}
