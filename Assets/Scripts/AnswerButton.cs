// This script is for the buttons the answers will go on

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnswerButton : MonoBehaviour
{
    public int Score = 0;
    public bool isCorrect;
    [SerializeField] public TextMeshProUGUI answerText;

    [SerializeField] private QuestionSetup questionSetup;


    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if(isCorrect)
        {
            Score = Score + 1;
            //Debug.Log("CORRECT ANSWER " + Score);

        }
        else
        {
            Debug.Log("WRONG ANSWER");
        }  
        if (questionSetup.questions.Count > 0)
        {
            // Generate a new question
            questionSetup.Start();
        }   
        else
        {
            Debug.Log("NO MORE QUESTIONS");
            Debug.Log(Score);
            //SceneManager.LoadSceneAsync(0);
        }
    }
    
    
}