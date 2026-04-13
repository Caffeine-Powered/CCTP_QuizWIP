using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickNEW : MonoBehaviour
{
    public GetData getdata;
    public GameObject[] toDestroy;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>();
    }

    public void OnClick()
    {
        StartCoroutine (WaitAndDestroy());
        Debug.Log("Score: " + getdata.score);
    }


    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Waited");
        toDestroy = GameObject.FindGameObjectsWithTag(getdata.currentQuestion.ToString());
        foreach(GameObject obj in toDestroy)
        {
            Destroy(obj);
        }
        getdata.currentQuestion++;
        //Debug.Log(getdata.currentQuestion);
        getdata.DisableQuestions();
        getdata.EndGameCheck();
        Debug.Log("CurrentQ: " + getdata.currentQuestion);
    }
}


