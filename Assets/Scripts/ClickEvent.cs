using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class ClickEvent : MonoBehaviour
{
    public GameObject[] cObject;
    public GameObject[] objects;
    public GetData getdata;
    


    public void Start()
    {
        getdata = FindObjectOfType<GetData>();
    }

    public void OnClick()
    {

        StartCoroutine (WaitAndDestroy());
    }

    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Waited");
        objects = GameObject.FindGameObjectsWithTag("QuestionBox");
            foreach(GameObject obj in objects)
            {
                Destroy(obj);
                
            }
        
        cObject = GameObject.FindGameObjectsWithTag("CorrectAnswer");
            foreach(GameObject obj in cObject)
            {
                Destroy(obj);
                if (getdata.currentQuestion >= getdata.numOfQuestions)
                {
                    Debug.Log(getdata.currentQuestion);
                }
            }
        Debug.Log("current question: " + getdata.currentQuestion);
        //getdata.ReadJSON();

    }
}
