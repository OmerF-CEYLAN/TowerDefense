using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject spawnPoint;

    [SerializeField] GameObject vehicle;

    [SerializeField] float spawnTime;

    Tower thisTower;

    float counter;

    VehicleUpgradeData vehicleUpgradeData;

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
        Vehicle spawnedVehicle = Instantiate(vehicle, spawnPoint.transform.position, Quaternion.identity).GetComponent<Vehicle>();

        spawnedVehicle.militaryBaseTower = thisTower;

        SetVehicleProperties(spawnedVehicle);
    }

    void SetVehicleProperties(Vehicle spawnedVehicle)
    {
        if(vehicleUpgradeData != null)
        {
            spawnedVehicle.Speed = vehicleUpgradeData.speed;
            spawnedVehicle.Health = vehicleUpgradeData.health;
            spawnedVehicle.FireRate = vehicleUpgradeData.fireRate;
            spawnedVehicle.FirePower = vehicleUpgradeData.firePower;
            spawnedVehicle.Range = vehicleUpgradeData.range;
        }

    }

    public void SetVehicleData(VehicleUpgradeData vehicleData)
    {
        vehicleUpgradeData = vehicleData;
    }

}
