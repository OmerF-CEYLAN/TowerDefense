using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panels;

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


}
