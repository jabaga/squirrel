using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float timeToLoadScene;
    public string sceneToLoad;
    public bool onEnable = false;
    
    void Start()
    {
        if(onEnable == false)
            Invoke("Load", timeToLoadScene);
    }

    private void OnEnable()
    {
        if (onEnable == true)
            Invoke("Load", timeToLoadScene);
    }

    void Load()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
