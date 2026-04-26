using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToMenu : MonoBehaviour
{
     public void OnClick()
    {
        SceneManager.LoadSceneAsync(3); //change scene to help scene
    }
}
