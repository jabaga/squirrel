using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    
    public static GameObject player
    {
        get { return GameObject.FindGameObjectWithTag("Player"); }
    }
    public static Canvas canvas
    {
        get { return GameObject.FindObjectOfType<Canvas>(); }
    }

    public static bool gameOver;

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

    public static void UpdateUIData()
    {
        canvas.transform.Find("TextLives").GetComponent<Text>().text = "Lives: " + PlayerData.currentLifes.ToString();
        canvas.transform.Find("TextBullets").GetComponent<Text>().text = "Acorns: " + PlayerData.currentBullets.ToString();
    }

    public static void GameOver()
    {
        gameOver = true;
        //display GameOverScreen
        SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

}
