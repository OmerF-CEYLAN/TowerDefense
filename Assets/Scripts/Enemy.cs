using DG.Tweening;
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

    public int currentPathIndex = 1;

    Rigidbody rb;

    public int HitPoint {  get => hitPoint; set => hitPoint = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);
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

        if (HitPointPanel.activeSelf)
        {
            HealthBarLookAtPlayer();
            SetHitPointUI();
        }


    }

    void Move()
    {
        if (currentPathIndex >= EnemyPathHolder.Instance.pathPoints.Count)
            return;

        Transform target = EnemyPathHolder.Instance.pathPoints[currentPathIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= speed * Time.deltaTime)
        {
            currentPathIndex++;

            if (currentPathIndex < EnemyPathHolder.Instance.pathPoints.Count)
            {
                transform.LookAt(EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);
            }
        }

    }

    public int GetHit(int firePower)
    {
        if (hitPoint <= 0) return 0;

        int hitPointBeforeHit = hitPoint; 

        hitPoint -= firePower;

        if (hitPointBeforeHit >= firePower)
            return firePower;
        else 
            return hitPointBeforeHit;

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
        return Vector3.Distance(transform.position, EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);
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
