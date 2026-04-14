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
    public GameObject finishButton;
    public GameObject restartButton;
    public GameObject menuButton;
    private bool menuOpen;
    public GameObject hideUI;
    //public Text questionText;

    public GetData getdata;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>();
        scoreText.enabled = false;
        finalText.enabled = false;
        wrongText.enabled = false;
        finishButton.SetActive(false);
        restartButton.SetActive(false);
        menuOpen = false;
        hideUI.SetActive(false);
        
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
        finishButton.SetActive(true);

        if (getdata.score != getdata.numOfQuestions)
        {
            restartButton.SetActive(true);
        }
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

    public void MenuManager()
    {
        if (menuOpen == false)
        {
            hideUI.SetActive(true);
            restartButton.SetActive(true);
            finishButton.SetActive(true);
            menuOpen = true;
        }
        else if (menuOpen == true)
        {
            hideUI.SetActive(false);
            restartButton.SetActive(false);
            finishButton.SetActive(false);
            menuOpen = false;
        }
    }
}
