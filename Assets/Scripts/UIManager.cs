using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;

    [SerializeField] GameObject winPanel;

    [SerializeField] GameObject losePanel;

    [SerializeField] string gameScene;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void SetLoseUI()
    {
        CloseAllPanels();
        losePanel.SetActive(true);
    }

    void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }


}
