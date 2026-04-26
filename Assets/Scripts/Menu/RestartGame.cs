using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
     public void OnClick()
    {
        SceneManager.LoadSceneAsync(1); //Load Active Quiz scene (restarts current scene)
    }
}