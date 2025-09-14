using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] int hitPoint;

    int maxHitPoint;

    [SerializeField]
    GameObject HitPointPanel;

    [SerializeField]
    TextMeshProUGUI hitPointText;

    [SerializeField]
    Image hitpointBar;

    [SerializeField] float barChangeSpeed;

    [SerializeField] GameObject bloodEffect;

    [SerializeField] List<AudioClip> zombieSounds;

    [SerializeField] AudioSource audioSource;

    float soundRate;

    float counter;

    public int currentPathIndex = 1;

    Rigidbody rb;

    public int HitPoint {  get => hitPoint; set => hitPoint = value; }

    public float Speed { get => speed; set => speed = value; }

    void Start()
    {
        soundRate = Random.Range(5f, 25f);

        rb = GetComponent<Rigidbody>();
        transform.LookAt(PathHolder.Instance.pathPoints[currentPathIndex].position);
        rb.linearVelocity = speed * transform.forward;

        maxHitPoint = hitPoint;
    }

    
    void Update()
    {
        if(hitPoint <= 0)
        {
            Destroy(gameObject);
        }

        Move();

        MakeSound();

        if (HitPointPanel.activeSelf)
        {
            HealthBarLookAtPlayer();
            SetHitPointUI();
        }


    }

    void Move()
    {
        if (currentPathIndex >= PathHolder.Instance.pathPoints.Count)
            return;

        Transform target = PathHolder.Instance.pathPoints[currentPathIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= speed * Time.deltaTime)
        {
            currentPathIndex++;

            if (currentPathIndex < PathHolder.Instance.pathPoints.Count)
            {
                transform.LookAt(PathHolder.Instance.pathPoints[currentPathIndex].position);
            }
        }

    }

    void MakeSound()
    {
        counter += Time.deltaTime;

        if(counter >= soundRate)
        {
            counter = 0;
            soundRate = Random.Range(10f, 25f);

            AudioClip selectedClip = zombieSounds[Random.Range(0, zombieSounds.Count)];

            audioSource.PlayOneShot(selectedClip);
        }



    }

    public int GetHit(int damage)
    {
        if (hitPoint <= 0) return 0;

        BloodEffectActivation();

        int hitPointBeforeHit = hitPoint; 

        hitPoint -= damage;

        if (hitPointBeforeHit >= damage)
            return damage;
        else 
            return hitPointBeforeHit;

    }

    void BloodEffectActivation()
    {
        ParticleSystem ps = bloodEffect.GetComponent<ParticleSystem>();

        ps.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            PlayerHealthHandler.Instance.Health -= hitPoint;
            PlayerHealthHandler.Instance.SetHealthText();

            Destroy(gameObject);
        }
    }

    public float GetDistanceToNextPath()
    {
        return Vector3.Distance(transform.position, PathHolder.Instance.pathPoints[currentPathIndex].position);
    }

    public void SetHitPointUI()
    {
        hitPointText.text = hitPoint + " / " + maxHitPoint;

        DOTween.To(() => hitpointBar.fillAmount, x => hitpointBar.fillAmount = x, (float)hitPoint / maxHitPoint, barChangeSpeed);
    }

    void HealthBarLookAtPlayer()
    {
        HitPointPanel.transform.LookAt(Camera.main.transform.position);

        HitPointPanel.transform.Rotate(0, 180, 0);
    }

    public void EnableHitPointUI()
    {
        HitPointPanel.SetActive(true);
        SetHitPointUI();
    }

    public void DisableHitPointUI()
    {
        HitPointPanel.SetActive(false);
    }

}
