using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickNEW : MonoBehaviour
{
    public GetData getdata;
    public Text correctText;
    public GameObject[] toDestroy;
    public HideShowUI showUI;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>();
        showUI = FindObjectOfType<HideShowUI>();
        showUI.hideCorrect();
    }

    public void OnClick()
    {
        showUI.showCorrect();
        StartCoroutine (WaitAndDestroy());
        Debug.Log("Score: " + getdata.score);
        //Debug.Log("Question :" + getdata.questionUI);

    }


    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log("Waited");
        showUI.hideCorrect();
        toDestroy = GameObject.FindGameObjectsWithTag(getdata.currentQuestion.ToString());
        foreach(GameObject obj in toDestroy)
        {
            Destroy(obj);
        }
        getdata.currentQuestion++; //this one cycles  the questions
        //showUI.questionText.text = getdata.questionUI;

        getdata.DisableQuestions();
        getdata.EndGameCheck();

    }

}


