using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
    public Button startButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        //startButton.onClick.AddListener(StartNewGame);
        //quitButton.onClick.AddListener(QuitGame);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("start_screen_scene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        
    }
}
