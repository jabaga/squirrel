using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float timeToLoadScene;
    public string sceneToLoad;
    
    void Start()
    {
        Invoke("Load", timeToLoadScene);
    }

    void Load()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
