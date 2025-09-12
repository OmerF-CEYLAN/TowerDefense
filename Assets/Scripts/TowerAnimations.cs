using UnityEngine;

public class TowerAnimations : MonoBehaviour
{
    [SerializeField] GameObject body;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShootAnim()
    {
        if (body != null)
        {
            if (body.TryGetComponent(out Animator animator))
            {
                animator.Play("Shoot", 0, 0f);
            }
        }
    }
}
