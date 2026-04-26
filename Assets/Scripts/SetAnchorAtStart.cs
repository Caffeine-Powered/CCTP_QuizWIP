using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SetAnchorAtStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.AddComponent<ARAnchor>(); //gives the object script is attached to an AR anchor (stops the object from moving/makes other objects anchor to it) 
    }
}
