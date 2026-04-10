using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GetData : MonoBehaviour
{
    public string DataURL;
    public int numOfQuestions;
    public int numOfAnswers;
    public GameObject QText;
    public GameObject[] answersInstances;
    public int currentQuestion;
    private int counter;
    private int txtcounter;
    public GameObject[] imageInstances;
    public string questionTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 0;
        txtcounter = 0;
        currentQuestion = 0;
        StartCoroutine(getData());
        Debug.Log("Start");
    }

    IEnumerator getData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(DataURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log(json); 
                ReadJSON(json);
            }
        }
    }

    
    public void ReadJSON(string jsonString) 
    {
        JSONNode node = JSON.Parse(jsonString);
        JSONObject obj = node.AsObject;
        
    
        numOfQuestions = (obj["Questions"].Count);
        //Debug.Log(numOfQuestions);
        numOfAnswers = (obj["Questions"][currentQuestion].Count - 1);
        //Debug.Log("num of answers " + numOfAnswers );

        for (int i = 0; i < numOfQuestions; i++)    //instantiate all of them/ tag and deactivate per question
        {
            for (int a = 0; a < numOfAnswers; a++)
            {
                string correctAnswer = obj["Questions"][currentQuestion]["Correct Answer"].Value;
                string wrongAnswer1 = obj["Questions"][currentQuestion]["Incorrect Answer 1"].Value;
                string wrongAnswer2 = obj["Questions"][currentQuestion]["Incorrect Answer 2"].Value;
                string wrongAnswer3 = obj["Questions"][currentQuestion]["Incorrect Answer 3"].Value;

                Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(0.0f, 3.0f), Random.Range(-3.0f, 3.0f));
                GameObject myText = Instantiate(QText, position, Quaternion.identity);
                TextMeshPro textComponent = myText.GetComponent<TextMeshPro>();
                textComponent.text = correctAnswer;
                questionTag = currentQuestion.ToString();
                myText.tag = questionTag;
                switch (txtcounter)
                {
                    case 0:
                        textComponent.text = correctAnswer; //if can add tag can have onclick event apply to this
                        break;
                    case 1:
                        textComponent.text = wrongAnswer1;
                        break;
                    case 2:
                        textComponent.text = wrongAnswer2;
                        break;
                    case 3:
                        textComponent.text = wrongAnswer3;
                        break;
                }
                txtcounter++;
                Debug.Log(txtcounter);
                if (counter >= answersInstances.Length)
                {
                    counter = 0; //counter should be numOfAnswers
                }
                GameObject qBox = Instantiate (answersInstances[counter], position, Random.rotation); //make a new qbox
                qBox.tag = questionTag;
                counter++; 
            }
            currentQuestion++;
        }
        
    }
}
