using UnityEngine;
using System.Collections;

public class MaterialInstance : MonoBehaviour
{
    public GameObject Instance;
    public Material material;
    public GetData getdata;
    public DownloadImage downloadImage;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        downloadImage = FindObjectOfType<DownloadImage>();
        Instance = this.gameObject;
        material = Instance.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //material = downloadImage.texture;
    }
}
