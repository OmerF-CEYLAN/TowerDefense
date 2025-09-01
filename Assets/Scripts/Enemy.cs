using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed, hitPoint;

    int currentPathIndex = 1;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);
        rb.linearVelocity = speed * transform.forward;
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(currentPathIndex < EnemyPathHolder.Instance.pathPoints.Count)
        {

            float distanceToPathWay = Vector3.Distance(transform.position, EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);

            if (distanceToPathWay <= 0.05f)
            {
                currentPathIndex++;

                transform.LookAt(EnemyPathHolder.Instance.pathPoints[currentPathIndex].position);

                rb.linearVelocity = speed * transform.forward;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            Destroy(gameObject);
        }
    }

}
