using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : Singleton<Main>
{
    public Canvas canvas;

    public GameObject player;

    public bool gameOver;

    public Button startButton, quitButton;

    // (Optional) Prevent non-singleton constructor use.
    protected Main() {
       
    }

    void Start()
    {
        //Application.LoadLevel("GameStartScreen");
        UpdateUIData();
    }

    void NewGame()
    {
        //Application.LoadLevel("Ivan");
    }

    public void UpdateUIData()
    {
        canvas.transform.Find("TextLives").GetComponent<Text>().text = "Lives: " + PlayerData.currentLifes.ToString();
        canvas.transform.Find("TextBullets").GetComponent<Text>().text = "Acorns: " + PlayerData.currentBullets.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        //display GameOverScreen
        Application.LoadLevel("GameOverScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
