using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickWrong : MonoBehaviour
{
    public GetData getdata;
    public Text correctText;
    public GameObject[] toDestroy;
    public HideShowUI showUI;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>(); //finds getdata script
        showUI = FindObjectOfType<HideShowUI>(); //finds hideshowui script
        showUI.hideWrong(); //runs the hideCorrect function in hideshowui script
    }

    public void OnClick()
    {
        showUI.showWrong(); //runs showWrong script in hideshowui script
        StartCoroutine (WaitAndDestroy()); //runs wait and destroy coroutine
        Debug.Log("Score: " + getdata.score); //prints score in console
    }


    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2); //waits 2 seconds
        showUI.hideWrong(); //runs the hideWrong function in hideshowui script
        toDestroy = GameObject.FindGameObjectsWithTag(getdata.currentQuestion.ToString()); //adds objects with the same tag number as current question to the toDestroy list
        foreach(GameObject obj in toDestroy) //for every object in toDestroy list
        {
            Destroy(obj); //destroys objects in the toDestroy list
        }
        getdata.currentQuestion++; //this one cycles the questions by incrementing the current quesiton number
        getdata.DisableQuestions(); //runs the DisableQuestion function in th getdata script
        getdata.EndGameCheck(); //runs the EndGameCheck function in the getdata script
    }
}


