using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] List<GameObject> towerPrefabs;
    [SerializeField] List<Button> buttons;

    GameObject currentTower;
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
        LockButtonsIfNotEnoughMoney();

        if (currentTower == null) return;

        // Her karede mouse pozisyonunu güncelle
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, LayerMask.GetMask("Ground")))
        {
            currentTower.transform.position = hit.point + Vector3.up * (currentTower.transform.localScale.y/2f);
        }

        // Sol click ile yerleþtir
        if (EventSystem.current.IsPointerOverGameObject() == false && Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }

    }


    void StartPlacingTower(GameObject towerPrefab)
    {
        if (currentTower != null) Destroy(currentTower);
        currentTower = Instantiate(towerPrefab);
    }

    void PlaceTower()
    {
        if (currentTower == null) return;

        
        Tower towerScript = currentTower.GetComponent<Tower>();

        if (CheckForTowersNearBy(towerScript) || CheckForRoad(towerScript))
            return;

        if (MoneyManager.Instance.HaveEnoughMoney(towerScript.StartCost) == false)
            return;


        if (towerScript != null)
        {
            MoneyManager.Instance.SpendMoney(towerScript.StartCost);

            towerScript.IsPlaced = true;

            currentTower.GetComponent<Collider>().enabled = true;

            currentTower = null;
        }

    }

    void LockButtonsIfNotEnoughMoney()
    {
        for (int i = 0; i < buttons.Count; i++)
        {

            Tower tower = towerPrefabs[i].GetComponent<Tower>();

            if (MoneyManager.Instance.HaveEnoughMoney(tower.StartCost))
                buttons[i].interactable = true;
            else
                buttons[i].interactable = false;

        }
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
