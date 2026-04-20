using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using  UnityEngine.UI;

public class GetData : MonoBehaviour
{
    public string DataURL;
    public int numOfQuestions;
    public int numOfAnswers;
    public int currentQuestion;
    private int counter;
    private int txtcounter;
    private int imgcounter;
    public int score;
    public GameObject QText;
    public GameObject scoreText;
    public string questionTag;
    public string questionUI;
    private GameObject qBox;
    public Vector3 position;
    public Vector3 Qposition;
    public GameObject QUIText;
    public GameObject[] answersInstances;
    public GameObject[] activeObjects;
    public HideShowUI showUI;
    public bool Continue;
    public TextMeshPro textComponent;
    public string correctAnswer;
    public string wrongAnswer1;
    public string wrongAnswer2;
    public string wrongAnswer3;
    public int imagesLoaded; 


    public GameObject questionImage;
    public DownloadImage downloadedImage;
    public GameObject[] imageInstances;
    public Material material;
    public Color color;
    public Renderer meshRenderer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        imgcounter = 0;
        counter = 0;
        txtcounter = 0;
        currentQuestion = 0;
        Continue = false;
        imagesLoaded = 0;
        
        StartCoroutine(getData());
        downloadedImage = FindObjectOfType<DownloadImage>();
        showUI = FindObjectOfType<HideShowUI>();
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
        
        numOfQuestions = (obj["Questions"].Count);  //assigns number of questions int
        numOfAnswers = (obj["Questions"][currentQuestion].Count - 1); //assigns number of answers int and removes the question
        

        for (int i = 0; i < numOfQuestions; i++)    //instantiate all of them/ tag and deactivate per question
        {
            questionUI = obj["Questions"][currentQuestion]["Question"].Value;
            txtcounter = 0; //empties counter for textbox instances
            for (int a = 0; a < numOfAnswers; a++)  //for loop until a = number of answers
            {
                
                correctAnswer = obj["Questions"][currentQuestion]["Correct Answer"].Value; //assigns correct answer to string
                wrongAnswer1 = obj["Questions"][currentQuestion]["Incorrect Answer 1"].Value; //assigns wrong answer1 to string
                wrongAnswer2 = obj["Questions"][currentQuestion]["Incorrect Answer 2"].Value; //assigns wrong answer2 to string
                wrongAnswer3 = obj["Questions"][currentQuestion]["Incorrect Answer 3"].Value; //assigns wrong answer3 to string

                questionTag = currentQuestion.ToString(); //assigns questionTag string by turning currentquestion int and turning it into a string
                position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f)); //creates new vector with random range in parameters
                GameObject myText = Instantiate(QText, position, Quaternion.identity);  //instantiates new game object for answer text from the answer text prefab
                textComponent = myText.GetComponent<TextMeshPro>(); //finds the text prefab text
                imagesLoaded++;
                StartCoroutine(AddImage(position, questionTag));
                

                Debug.Log(textComponent.text);
                myText.tag = questionTag;   //assigns a tag to each new answer text prefab based on what question it was on in the for loop


                if (counter >= answersInstances.Length)
                {
                    counter = 0; //counter should be numOfAnswers
                }
                qBox = Instantiate (answersInstances[counter], position, Random.rotation); //make a new qbox
                qBox.tag = questionTag;

                counter++;
                txtcounter++; //increments txtcounter
                //Continue = false;
               // }
            }
            Qposition = new Vector3((0.0f), (0.0f)  + 100.0f, (3.0f)); //creates new vector with random range in parameters
            GameObject QuesText = Instantiate(QUIText, Qposition, Quaternion.identity);  //instantiates new game object for answer text from the answer text prefab
            TextMeshPro QUItextcomponent = QuesText.GetComponent<TextMeshPro>(); //finds the text prefab text
            QUItextcomponent.text = questionUI;
            QuesText.tag = questionTag;

            currentQuestion++;
        }
        currentQuestion = 0;
        Debug.Log("currentQuestion: " + currentQuestion);
        Debug.Log(currentQuestion.ToString());
       

    }

    public void DisableQuestions()
    {
        activeObjects = GameObject.FindGameObjectsWithTag(currentQuestion.ToString());
        foreach(GameObject obj in activeObjects)
        {
            //Debug.Log("Tagged With: " + currentQuestion.ToString());
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y -100.0f, obj.transform.position.z);
        }
        Debug.Log("sada" + questionUI);
    }

    public void EndGameCheck()
    {
        if (currentQuestion == numOfQuestions)
        {
            string finalScore = score.ToString();
            Debug.Log(score + "/" + numOfQuestions);
            showUI.UION();
        }
    }

    IEnumerator AddImage(Vector3 spawnPosition, string questionTag)
    {
        switch (txtcounter) //switch statement for instances of answers from txtcounter
        {
            case 0:
                textComponent.text = correctAnswer; //assigns correct answer
                downloadedImage.imageToLoad = correctAnswer;


                break;
            case 1:
                textComponent.text = wrongAnswer1;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer1;

                break;
            case 2:
                textComponent.text = wrongAnswer2;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer2;

                break;
            case 3:
                textComponent.text = wrongAnswer3;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer3;
               // downloadedImage.ImageStart();
                //StartCoroutine(AddImage());
                break;
        }
        yield return StartCoroutine(downloadedImage.LoadImage(downloadedImage.imageToLoad));
        Texture tex = downloadedImage.texture;
        if (imgcounter >= answersInstances.Length)
        {
            imgcounter = 0; //counter should be numOfAnswers
        }
        position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f)); //creates new vector with random range in parameters
        Debug.Log("position used: " + position);
        GameObject questionImage = Instantiate (imageInstances[imgcounter], spawnPosition, Random.rotation);
        questionImage.tag = questionTag;
        yield return new WaitForSeconds(2);      
        Renderer rend = questionImage.GetComponent<Renderer>();
        rend.material = new Material(rend.material);
        rend.material.SetTexture("_BaseMap",tex);
        Debug.Log("Loaded Material");
        Debug.Log(textComponent.text);
        //Continue = true;
        imagesLoaded--;
        if (imagesLoaded == 0)
        {
            DisableQuestions();
        }
    }





}
