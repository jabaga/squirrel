using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : Singleton<Main>
{
    public Canvas canvas;

    public GameObject player;

    public bool gameOver;

    // (Optional) Prevent non-singleton constructor use.
    protected Main() {
       
    }

    void Start()
    {
        gameOver = false;
        UpdateUIData();
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
    }

}
