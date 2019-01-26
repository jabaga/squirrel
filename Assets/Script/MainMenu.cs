using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
    public Button startButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartNewGame()
    {
        Application.LoadLevel("Ivan");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        
    }
}
