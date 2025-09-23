using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnGameWin()
    {
        uiManager.SeWinUI();
    }
    
    public void OnGameLose()
    {
        uiManager.SetLoseUI();
    }

}
