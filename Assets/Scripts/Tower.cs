using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float fireRate, firePower, startCost, range;

    public bool IsPlaced { get; set; }

    void Update()
    {
        if (!IsPlaced)
            return;

        // burada ateþ etme, düþman tarama vs. yapýlacak
    }
}
