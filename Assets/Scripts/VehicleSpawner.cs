using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject spawnPoint;

    [SerializeField] GameObject vehicle;

    [SerializeField] float spawnTime;

    Tower thisTower;

    float counter;

    void Start()
    {
        spawnPoint = GameObject.Find("EndPoint");
        counter = spawnTime;
        thisTower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTower.IsPlaced == false)
            return;

        counter += Time.deltaTime;

        if(counter >= spawnTime)
        {
            counter = 0;
            SpawnVehicle();
        }
    }

    void SpawnVehicle()
    {
        Instantiate(vehicle, spawnPoint.transform.position, Quaternion.identity);
    }

}
