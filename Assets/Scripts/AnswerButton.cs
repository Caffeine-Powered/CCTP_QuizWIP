// This script is for the buttons the answers will go on

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class AnswerButton : MonoBehaviour
{
    

    public bool isCorrect;
    [SerializeField] public TextMeshProUGUI answerText;

    [SerializeField] private QuestionSetup questionSetup;
    Scoreboard scoreboardUpdate;

    void Start()
    {
        scoreboardUpdate = GameObject.FindGameObjectWithTag("Score").GetComponent<Scoreboard>();
    }

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
            Debug.Log("CORRECT ANSWER");
            scoreboardUpdate.UpdateScore();
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
            SceneManager.LoadSceneAsync(2);
        }
    }
    
    
}