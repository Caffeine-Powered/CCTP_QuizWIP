using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadYourAsyncScene()); //run loadyourasyncscene function on start
    }

IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1); //Load scenes asynchronously while running
        while (!asyncLoad.isDone)         // Wait until the asynchronous scene fully loads
        {
            yield return null;
        }
    }
}


