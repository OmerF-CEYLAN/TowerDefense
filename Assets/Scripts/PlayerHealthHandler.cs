using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{

    [SerializeField] int health;

    int maxHealth;

    public int Health { get => health; set => health = value; }

    public static PlayerHealthHandler Instance;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] Image healthBar;

    [SerializeField] float barChangeSpeed;

    [SerializeField] bool infiniteHealth;

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
        if (infiniteHealth)
        {
            health = 10000;
            return;
        }
            

        if (health < 0)
        {
            health = 0;

            GameManager.Instance.OnGameLose();
        }
    }

    public void SetHealthText()
    {
        healthText.text = health + " / " + maxHealth;

        DOTween.To(() => healthBar.fillAmount, x => healthBar.fillAmount = x, (float)health / maxHealth, barChangeSpeed);
    }



}
