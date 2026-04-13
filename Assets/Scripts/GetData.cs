using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

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
    private int imgcounter;
    public int score;
    public string questionTag;
    private GameObject qBox;
    public Vector3 position;
    public Vector3 Qposition;
    public GameObject QUIText;
    public GameObject[] UIInstances;
    public GameObject[] activeObjects;

    public DownloadImage downloadedImage;
    public GameObject questionImage;
    public GameObject[] imageInstances;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        imgcounter = 0;
        counter = 0;
        txtcounter = 0;
        currentQuestion = 0;
        StartCoroutine(getData());
        downloadedImage = FindObjectOfType<DownloadImage>();
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
            string questionUI = obj["Questions"][currentQuestion]["Question"].Value;
            txtcounter = 0;
            for (int a = 0; a < numOfAnswers; a++)
            {
                
                string correctAnswer = obj["Questions"][currentQuestion]["Correct Answer"].Value;
                string wrongAnswer1 = obj["Questions"][currentQuestion]["Incorrect Answer 1"].Value;
                string wrongAnswer2 = obj["Questions"][currentQuestion]["Incorrect Answer 2"].Value;
                string wrongAnswer3 = obj["Questions"][currentQuestion]["Incorrect Answer 3"].Value;

                
                
                Vector3 position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f));
                GameObject myText = Instantiate(QText, position, Quaternion.identity);
                TextMeshPro textComponent = myText.GetComponent<TextMeshPro>();
                
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
                
                Debug.Log(textComponent.text);
                txtcounter++;
                questionTag = currentQuestion.ToString();
                myText.tag = questionTag;

                //Vector3 position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f));
                
                //Debug.Log(txtcounter);
                if (counter >= answersInstances.Length)
                {
                    counter = 0; //counter should be numOfAnswers
                }
                GameObject qBox = Instantiate (answersInstances[counter], position, Random.rotation); //make a new qbox
                qBox.tag = questionTag;

                GameObject questionImage = Instantiate (imageInstances[0], position, Random.rotation);
                questionImage.tag = questionTag;
                //downloadedImage.imageToLoad = "https://upload.wikimedia.org/wikipedia/commons/a/a4/Jempol_Ngadep_Atas_%28cropped%29.jpg";
                //downloadedImage.ImageStart();
                counter++;

            }
            position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f));
            GameObject quiText = Instantiate(QUIText, position, Quaternion.identity);
            TextMeshPro uiText = quiText.GetComponent<TextMeshPro>();
            uiText.text = questionUI;
            currentQuestion++;
        }
        currentQuestion = 0;
        Debug.Log("currentQuestion: " + currentQuestion);
        Debug.Log(currentQuestion.ToString());
        DisableQuestions();

    }

    public void DisableQuestions()
    {
        activeObjects = GameObject.FindGameObjectsWithTag(currentQuestion.ToString());
        foreach(GameObject obj in activeObjects)
        {
            //Debug.Log("Tagged With: " + currentQuestion.ToString());
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y -100.0f, obj.transform.position.z);
        }
        
    }

    public void EndGameCheck()
    {
        if (currentQuestion == numOfQuestions)
        {
            SceneManager.LoadSceneAsync(2);
            Debug.Log("Scenechange");
        }
    }
}
