using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;

    [SerializeField] GameObject winPanel;

    [SerializeField] GameObject losePanel;

    [SerializeField] string gameScene;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        soundSlider.value = PlayerPrefs.GetFloat("Sounds", 1f);

        musicSlider.onValueChanged.AddListener(SoundsManager.Instance.SetMusicValue);
        soundSlider.onValueChanged.AddListener(SoundsManager.Instance.SetSoundValue);
    }

    
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetLoseUI()
    {
        CloseAllPanels();
        losePanel.SetActive(true);
    }
    
    public void SeWinUI()
    {
        CloseAllPanels();
        winPanel.SetActive(true);
    }

    void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        CloseAllPanels();

        if(panels.Contains(panel))
            panel.SetActive(true);
    }


}
