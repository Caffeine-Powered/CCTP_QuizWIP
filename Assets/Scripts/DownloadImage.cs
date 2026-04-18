using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] public Image imagePlaced;
    [SerializeField] public Renderer objRenderer;
    public Material material;
    public string imageToLoad;
    public Texture2D texture;
    public GetData getdata;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ImageStart()
    {
        imageToLoad = "https://as2.ftcdn.net/jpg/01/71/34/85/1000_F_171348575_B3XRv2OcHir9SjsM9lixthxQqyxBYq0a.jpg";
        StartCoroutine(LoadImage(imageToLoad));
    }

    public IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //getdata.material.material.mainTexture = texture;
        }
    }

}
