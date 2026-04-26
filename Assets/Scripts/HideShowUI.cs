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
        getdata = FindObjectOfType<GetData>(); //finds the getdata script
        scoreText.enabled = false;  //disables score text on start
        finalText.enabled = false;  //disables final text on start
        wrongText.enabled = false;  //disables wrong text on start
        finishButton.SetActive(false);  //disables finish button on start
        restartButton.SetActive(false); //disables restart button on start
        menuOpen = false; //sets menu to be closed on start
        hideUI.SetActive(false);    //disables ui on start
        
    }

    void Update()
    {
        scoreText.text = getdata.score.ToString() + " Out Of " + getdata.numOfQuestions.ToString(); //updates score text to current score out of number of questinos
    }

    public void UION()
    {
        scoreText.enabled = true; //enables score text
        finalText.enabled = true; //enables final text
        finishButton.SetActive(true); //enables finish button

        if (getdata.score != getdata.numOfQuestions) //if player score is less than number of questions (doesn't appear if player has max score)
        {
            restartButton.SetActive(true); //enables retry button
        }
    }
    
    public void showCorrect()
    {
        correctText.enabled = true; //enables correct text
    }

    public void hideCorrect()
    {
        correctText.enabled = false;    //hides correct text
    }
    
        public void showWrong()
    {
        wrongText.enabled = true; //enables wrong text
    }

    public void hideWrong()
    {
        wrongText.enabled = false; //hides wrong text
    }

    public void MenuManager()
    {
        if (menuOpen == false) //in the menu is disabled
        {
            hideUI.SetActive(true); //enables ui
            restartButton.SetActive(true); //enables restart button
            finishButton.SetActive(true); //enables finish button
            menuOpen = true; //menuOpen state set to true
        }
        else if (menuOpen == true) //if the menu is enabled
        {
            hideUI.SetActive(false); //disables ui
            restartButton.SetActive(false); //disables restart button
            finishButton.SetActive(false); //disables finish button
            menuOpen = false; //menuOpen state set to false
        }
    }
}
