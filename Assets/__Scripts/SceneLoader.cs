using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);//incremeent the scene by 1 everytime 
    }

    //Loading the scene 0 when first starting  
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
