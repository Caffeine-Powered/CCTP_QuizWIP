using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;


public class EnableDisableQuestions : MonoBehaviour
{
    public GetData getdata;
    
  public void Start()
    {
        getdata = FindObjectOfType<GetData>();
        Debug.Log("This tag = " + this.tag);
    }

    // Update is called once per frame
    void Update()
    {
        /**
        Debug.Log("currentQuestion: " + getdata.currentQuestion);
        if (this.tag != getdata.currentQuestion.ToString())
        {
           // SetActive(false);
        }
        **/
    }
}
