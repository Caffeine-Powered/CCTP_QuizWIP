using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    public Image imagePlaced;
    public Renderer objRenderer;
    public string imageToLoad;
    public Texture texture;
    public GetData getdata;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ImageStart()
    {
        StartCoroutine(LoadImage(imageToLoad)); //runs loadimage coroutine
    }

    public IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url); //makes webrequest using the URL from the variable
        yield return www.SendWebRequest();  //sends web request

        if(www.result != UnityWebRequest.Result.Success) //if webrequest not successful
        {
            Debug.Log(www.error); //print error message in console
        }
        else //else
        {
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture; //converts downloaded image into a texture
            Debug.Log("Assigned Material"); //prints message in console
        }
    }

}
