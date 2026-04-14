using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] public Image imagePlaced;
    [SerializeField] public Renderer objRenderer;
    public string imageToLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ImageStart()
    {
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

            //put texture onto image
            //Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height),new Vector2());
            //imagePlaced.sprite = sprite;
            //imagePlaced.preserveAspect = true;

            //put texture onto sphere
            objRenderer.material.mainTexture = texture;

        }
    }

}
