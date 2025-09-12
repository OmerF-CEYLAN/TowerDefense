using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{

    [SerializeField] GameObject body;

    float speed;

    int speedHash;

    void Start()
    {
        if(body != null)
        {
            if(body.TryGetComponent(out Animator animator))
            {
                speedHash = Animator.StringToHash("Speed");

                speed = GetComponent<Enemy>().Speed;

                animator.SetFloat(speedHash, speed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
