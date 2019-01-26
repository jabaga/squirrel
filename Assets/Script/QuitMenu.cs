using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{
    public Button restartButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void RestartGame()
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
