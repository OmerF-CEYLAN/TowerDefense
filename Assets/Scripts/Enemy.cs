using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed, hitPoint;

    float maxHitPoint;

    [SerializeField]
    GameObject HitPointPanel;

    [SerializeField]
    TextMeshProUGUI hitPointText;

    [SerializeField]
    Image hitpointBar;



    public int currentPathIndex = 1;

    Rigidbody rb;

    public float HitPoint {  get => hitPoint; set => hitPoint = value; }

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

        hitpointBar.fillAmount = hitPoint / maxHitPoint;
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
