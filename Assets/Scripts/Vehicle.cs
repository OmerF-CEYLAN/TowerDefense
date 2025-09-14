using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : TowerBase
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip engineSound;
    [SerializeField] GameObject explosionObject;
    [SerializeField] Renderer vehicleRenderer;
    [SerializeField] GameObject shotEffectObject;

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

    [SerializeField] List<GameObject> tires;

    [SerializeField] float destroyDelay;

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
        audioSource.clip = engineSound;

        audioSource.Play();

        audioSource.loop = true;

        explosionObject.SetActive(false);

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
            SetHealthUI();

            GetComponent<Collider>().enabled = false;

            audioSource.loop = false;
            audioSource.Stop();

            audioSource.PlayOneShot(explosionSound);
            explosionObject.SetActive(true);

            foreach (GameObject tire in tires)
            {
                tire.SetActive(false);
            }

            float randonAngle = Random.Range(-45f, 45f);
            transform.Rotate(Vector3.up * randonAngle);

            rb.linearVelocity = transform.forward * speed;

            vehicleRenderer.material.DOColor(Color.black, 1f);

            DOTween.To(() => rb.linearVelocity, x => rb.linearVelocity = x, Vector3.zero, destroyDelay / 1.5f);

            Destroy(gameObject, destroyDelay);

            this.enabled = false;
            return;
        }

        RotateTires();
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
                counter = 0f;
                int dealedDamage = target.GetHit(firePower);

                audioSource.PlayOneShot(shotSound);
                ShotEffectActivation();

                totalDamage += dealedDamage;

                militaryBaseTower.TotalDamage += dealedDamage;

                MoneyManager.Instance.AddMoney(dealedDamage);
            }

        }
    }

    public void CrashToEnemy(Enemy crashedEnemy)
    {
        int dealedDamage = health >= crashedEnemy.HitPoint ? crashedEnemy.HitPoint : health;

        health -= dealedDamage;

        health = Mathf.Clamp(health, 0,200);

        militaryBaseTower.TotalDamage += dealedDamage;

        audioSource.PlayOneShot(crashSound);

        MoneyManager.Instance.AddMoney(dealedDamage);

        crashedEnemy.GetHit(dealedDamage);
    }

    void ShotEffectActivation()
    {
        ParticleSystem ps = shotEffectObject.GetComponent<ParticleSystem>();

        ps.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }

        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            CrashToEnemy(enemy);
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

    void RotateTires()
    {
        foreach (var item in tires)
        {
            item.transform.Rotate(Vector3.forward * 15f,Space.Self);    
        }
    }



}
