using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUIController : MonoBehaviour
{

    [SerializeField] GameObject towerUpgradePanel;

    [SerializeField] TextMeshProUGUI firePowerText;

    [SerializeField] TextMeshProUGUI fireRateText;

    [SerializeField] TextMeshProUGUI rangeText;

    [SerializeField] TextMeshProUGUI totalDamageText;

    [SerializeField] TextMeshProUGUI upgradeCostText;

    [SerializeField] TextMeshProUGUI upgradeText;

    [SerializeField] TextMeshProUGUI sellCostText;

    [SerializeField] Button sellButton;

    [SerializeField] Button upgradeButton;

    TowerPlacementManager placementManager;

    Tower thisTower;

    [SerializeField] LineRenderer lr;

    [SerializeField] int segments = 60;
    [SerializeField] float lineWidth = 0.05f;

    void Awake()
    {
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
    }

    void Start()
    {
        thisTower = GetComponent<Tower>();
        lr.gameObject.SetActive(true);
        placementManager = GameObject.Find("TowerPlacementManager").GetComponent<TowerPlacementManager>();
        SetTowerUI();

        if (thisTower.IsPlaced == false)
        {
            sellButton.interactable = false;
            upgradeButton.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (towerUpgradePanel.activeSelf)
        {
            if (thisTower.IsPlaced)
            {
                sellButton.interactable = true;
                upgradeButton.interactable = true;
            }

            TowerUpgradePanelLookAtPlayer();
            UpdateTotalDamageText();
        }
    }

    public void OnTowerUIEnabled()
    {
        
        towerUpgradePanel.SetActive(true);
        lr.gameObject.SetActive(true);
        SetTowerUI();
        
    }

    public void OnTowerUIDisabled()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        towerUpgradePanel.SetActive(false);
        lr.gameObject.SetActive(false);
    }

    public void SetTowerUI()
    {
        DrawRangeCircle(thisTower.Range);
        firePowerText.text = "Fire Power: " + thisTower.FirePower.ToString();
        fireRateText.text = "Fire Rate: " + thisTower.FireRate.ToString();
        rangeText.text = "Range: " + thisTower.Range.ToString();

        string moneyBack = ((int)(thisTower.TotalMoneySoent / 2f)).ToString();
        sellCostText.text = "$ " + moneyBack;

        if (thisTower.UpgradeCost > 0)
            upgradeCostText.text = "$ " + thisTower.UpgradeCost.ToString();
        else if(thisTower.UpgradeCost == 0)
        {
            upgradeText.text = "Max";

            upgradeCostText.gameObject.SetActive(false);
        }

    }

    public void SellTowerButton()
    {
        placementManager.RemoveTower(thisTower);
    }

    void UpdateTotalDamageText()
    {
        totalDamageText.text = thisTower.TotalDamage.ToString();
    }

    void TowerUpgradePanelLookAtPlayer()
    {
        towerUpgradePanel.transform.LookAt(Camera.main.transform.position);

        towerUpgradePanel.transform.Rotate(0, 180, 0);
    }

    void SetSellText()
    {

    }

    public void DrawRangeCircle(float radius)
    {
        lr.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / segments) * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

}
