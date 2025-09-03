using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{

    [SerializeField] float health;

    float maxHealth;

    public float Health { get => health; set => health = value; }

    public static PlayerHealthHandler Instance;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] Image healthBar;

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

        maxHealth = health;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetHealthText()
    {
        healthText.text = health + " / " + maxHealth;

        healthBar.fillAmount = health / maxHealth;
    }



}
