using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] List<GameObject> towerPrefabs;
    [SerializeField] List<Button> buttons;

    GameObject currentTower;   // hen�z yerle�tirilmemi� kule
    Camera cam;

    void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => StartPlacingTower(towerPrefabs[index]));
        }
    }

    void Update()
    {
        if (currentTower == null) return;

        // Her karede mouse pozisyonunu g�ncelle
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, LayerMask.GetMask("Ground")))
        {
            currentTower.transform.position = hit.point + Vector3.up * (currentTower.transform.localScale.y/2f);
        }

        // Sol click ile yerle�tir
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }


    void StartPlacingTower(GameObject towerPrefab)
    {
        if (currentTower != null) Destroy(currentTower); // �nceki yerle�memi� kule varsa iptal et
        currentTower = Instantiate(towerPrefab);
    }

    void PlaceTower()
    {
        if (currentTower == null) return;

        
        Tower towerScript = currentTower.GetComponent<Tower>();

        if (CheckForTowersNearBy(towerScript) || CheckForRoad(towerScript))
            return;


        if (towerScript != null)
            towerScript.IsPlaced = true;

        currentTower.GetComponent<Collider>().enabled = true;

        currentTower = null; // art�k yerle�ti, mouse'u takip etmiyor
    }

    bool CheckForTowersNearBy(Tower currentTower)
    {
        currentTower.GetComponent<Collider>().enabled = false;
        return Physics.CheckSphere(currentTower.transform.position,0.3f,LayerMask.GetMask("Tower"));
    }

    bool CheckForRoad(Tower currentTower)
    {
        //return Physics.Raycast(currentTower.transform.position,Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Road"));
        return Physics.CheckSphere(currentTower.transform.position, 0.55f, LayerMask.GetMask("Road"));
    }

}
