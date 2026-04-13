using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class HideShowUI : MonoBehaviour
{
    public Text scoreText;
    public Text finalText;
    public Text correctText;
    public Text wrongText;
    public Text questionText;

    public GetData getdata;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>();
        scoreText.enabled = false;
        finalText.enabled = false;
        wrongText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = getdata.score.ToString() + " Out Of " + getdata.numOfQuestions.ToString();
        //questionText.text = getdata.questionUI;
    }

    public void UION()
    {
        scoreText.enabled = true;
        finalText.enabled = true;
    }

    public void showCorrect()
    {
        correctText.enabled = true;
    }

    public void hideCorrect()
    {
        correctText.enabled = false;
    }
    
        public void showWrong()
    {
        wrongText.enabled = true;
    }

    public void hideWrong()
    {
        wrongText.enabled = false;
    }
}
