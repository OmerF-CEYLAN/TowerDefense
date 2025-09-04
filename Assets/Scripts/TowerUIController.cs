using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUIController : MonoBehaviour
{

    [SerializeField] GameObject towerUpgradePanel;

    [SerializeField] TextMeshProUGUI FirePowerText;

    [SerializeField] TextMeshProUGUI FireRateText;

    [SerializeField] TextMeshProUGUI RangeText;

    Tower thisTower;

    void Start()
    {
        thisTower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (towerUpgradePanel.activeSelf)
        {
            TowerUpgradePanelLookAtPlayer();
        }
    }

    public void OnTowerUIEnabled()
    {
        
         towerUpgradePanel.SetActive(true);
         SetTowerUI();
        
    }

    public void OnTowerUIDisabled()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        towerUpgradePanel.SetActive(false);
    }

    public void SetTowerUI()
    {
        FirePowerText.text = "Fire Power: " + thisTower.FirePower.ToString();
        FireRateText.text = "Fire Rate: " + thisTower.FireRate.ToString();
        RangeText.text = "Range: " + thisTower.Range.ToString();
    }

    void TowerUpgradePanelLookAtPlayer()
    {
        towerUpgradePanel.transform.LookAt(Camera.main.transform.position);

        towerUpgradePanel.transform.Rotate(0, 180, 0);
    }

}
