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

    int currentPathIndex;

    Rigidbody rb;

    void Start()
    {
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

    public void CrashToEnemies(Enemy crashedEnemy)
    {
        int dealedDamage = health >= crashedEnemy.HitPoint ? crashedEnemy.HitPoint : health;

        health -= dealedDamage;

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

    public void SetHealthUI()
    {
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
        healthPanel.SetActive(true);
        SetHealthUI();
    }

    public void DisableHitPointUI()
    {
        healthPanel.SetActive(false);
    }



}
