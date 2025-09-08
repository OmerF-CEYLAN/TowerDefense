using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] InputHandler inputHandler;
    [SerializeField] float sensitivity;
    float xRotation;

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask towerLayer;

    Enemy hoveredEnemy;
    TowerBase hoveredTower;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();

        MouseHoverOnEnemy();
        MouseHoverOnTower();
    }

    void LookAround()
    {
        if(Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;

            Vector3 lookVector = inputHandler.GetLookAroundVector();

            float mouseX = lookVector.x * sensitivity;

            float mouseY = lookVector.y * sensitivity;

            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void MouseHoverOnEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit,10f, enemyLayer))
        {
            if(Input.GetMouseButtonDown(0) && hit.collider.TryGetComponent(out Enemy enemy))
            {
                if(hoveredEnemy != null)
                {
                    hoveredEnemy.DisableHitPointUI();
                }

                hoveredEnemy = enemy;
                enemy.EnableHitPointUI();
            }
        }
        else if(Input.GetMouseButtonDown(0) && hoveredEnemy != null)
        {
            hoveredEnemy.DisableHitPointUI();
        }

    }

    void MouseHoverOnTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f, towerLayer))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.TryGetComponent(out TowerBase tower))
            {
                if (hoveredTower != null)
                {
                    if (hoveredTower.TryGetComponent(out TowerUIController towerUIController))
                    {
                        towerUIController.OnTowerUIDisabled();
                    }
                    else if (hoveredEnemy.TryGetComponent(out Vehicle vehicleObj))
                    {
                        vehicleObj.DisableHitPointUI();
                    }
                }

                hoveredTower = tower;

                if(hoveredTower.TryGetComponent(out TowerUIController towerUI))
                {
                    towerUI.OnTowerUIEnabled();
                }
                else if(hoveredTower.TryGetComponent(out Vehicle vehicle))
                {
                    vehicle.EnableHitPointUI();
                }

            }
        }
        else if (Input.GetMouseButtonDown(0) && hoveredTower != null)
        {
            hoveredTower.GetComponent<TowerUIController>().OnTowerUIDisabled();
        }
    }


}
