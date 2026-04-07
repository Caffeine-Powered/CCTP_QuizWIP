using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] private Image imagePlaced;
    [SerializeField] private Renderer objRenderer;
    private string imageToLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageToLoad = "https://upload.wikimedia.org/wikipedia/commons/a/a4/Jempol_Ngadep_Atas_%28cropped%29.jpg";
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
            Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height),new Vector2());
            imagePlaced.sprite = sprite;
            imagePlaced.preserveAspect = true;

            //put texture onto sphere
            objRenderer.material.mainTexture = texture;
            //objRenderer.material.SetTexture("_MainTex", texture);
        }
    }

}
