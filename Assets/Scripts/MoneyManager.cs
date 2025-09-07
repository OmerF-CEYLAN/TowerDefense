using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int initialMoney;

    [SerializeField] TextMeshProUGUI moneyText;

    int currentMoney;

    public static MoneyManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentMoney = initialMoney;

        SetMoneyUI();
    }

    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        OnMoneyChange();
    }

    public void SpendMoney(int cost)
    {
        currentMoney -= cost;
        OnMoneyChange();
    }

    public bool HaveEnoughMoney(int cost)
    {
        if (cost <= currentMoney) return true;
        else return false;
    }

    public void OnMoneyChange()
    {
        SetMoneyUI();
    }

    void SetMoneyUI()
    {
        moneyText.text = "$ " + currentMoney.ToString();
    }


}
