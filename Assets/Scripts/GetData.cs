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
        score = 0;          //sets score to 0
        imgcounter = 0;     //sets imgcounter to 0
        counter = 0;        //sets counter to 0
        txtcounter = 0;     //sets text counter to 0;
        currentQuestion = 0;//sets currentquestion to 0;
        imagesLoaded = 0;   //sets images to imagesloaded to 0 for the ienumerator;
        
        StartCoroutine(getData());      //starts getdata coroutine to get the json from the googlesheets
        downloadedImage = FindObjectOfType<DownloadImage>(); //finds the dounloadimage script
        showUI = FindObjectOfType<HideShowUI>();    //finds the hide/show ui script
        Debug.Log("Start"); //puts "start" in console
    }

    IEnumerator getData()   //ienumerator for getting the json data---------------------------------------------///Rod's code start----
    {
        using (UnityWebRequest request = UnityWebRequest.Get(DataURL))  //makes webrequest for the spreadsheet
        {
            yield return request.SendWebRequest();  //waits until webrequest has finished before continuing

            if (request.result == UnityWebRequest.Result.ConnectionError) //if there is a connection error/can't connect
            {
                Debug.LogError(request.error); //print error in console
            }
            else //else
            {
                string json = request.downloadHandler.text; //put the string downloaded from the spreadsheet into a string
                Debug.Log(json);    //prints downloaded string into console to see if it's correct
                ReadJSON(json); //starts the readjson function with the json string passed into it
            }
        }
    }

    public void ReadJSON(string jsonString)         //readjson function with jsonstring passed into it
    {
        JSONNode node = JSON.Parse(jsonString);     //parses json string into a json node
        JSONObject obj = node.AsObject;             //turns json node into a json object ------------------------------///Rod's code end----
        
        numOfQuestions = (obj["Questions"].Count);  //assigns number of questions int
        numOfAnswers = (obj["Questions"][currentQuestion].Count - 1); //assigns number of answers int and removes the question
        

        for (int i = 0; i < numOfQuestions; i++)    //instantiate all of them/ tag and deactivate per question
        {
            questionUI = obj["Questions"][currentQuestion]["Question"].Value;   //assigns the questions into a string
            txtcounter = 0; //empties counter for textbox instances
            for (int a = 0; a < numOfAnswers; a++)  //for loop until a = number of answers
            {
                
                correctAnswer = obj["Questions"][currentQuestion]["Correct Answer"].Value; //assigns correct answer to string
                wrongAnswer1 = obj["Questions"][currentQuestion]["Incorrect Answer 1"].Value; //assigns wrong answer1 to string
                wrongAnswer2 = obj["Questions"][currentQuestion]["Incorrect Answer 2"].Value; //assigns wrong answer2 to string
                wrongAnswer3 = obj["Questions"][currentQuestion]["Incorrect Answer 3"].Value; //assigns wrong answer3 to string

                questionTag = currentQuestion.ToString(); //assigns questionTag string by turning currentquestion int and turning it into a string
                position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f)); //creates new vector with random range in parameters
                //GameObject myText = Instantiate(QText, position, Quaternion.identity);  //instantiates new game object for answer text from the answer text prefab
                //textComponent = myText.GetComponent<TextMeshPro>(); //finds the text prefab text
                imagesLoaded++; //increments images loaded counter
                StartCoroutine(AddImage(position, questionTag)); //starts coroutine for downloading and instantiating images with position and questiontag fed in
                

                //Debug.Log(textComponent.text);  //prints the text for each textcomponent on each loop in console
                //myText.tag = questionTag;   //assigns a tag to each new answer text prefab based on what question it was on in the for loop


                if (counter >= answersInstances.Length) //of the counter exceeds or is equal to the number of answers
                {
                    counter = 0; //counter set to 0
                }
                qBox = Instantiate (answersInstances[counter], position, Random.rotation); //make a new qbox
                qBox.tag = questionTag; //assigns the questiontag to the qbox (the tag is the based on the currentquestion i.e question 1 is tagged 0, question 2 as 1 etc.)

                counter++; //increment counter
                txtcounter++; //increments txtcounter
            }
            Qposition = new Vector3((0.0f), (0.0f)  + 100.0f, (3.0f)); //creates new vector with random range in parameters
            GameObject QuesText = Instantiate(QUIText, Qposition, Quaternion.identity);  //instantiates new game object for answer text from the answer text prefab
            TextMeshPro QUItextcomponent = QuesText.GetComponent<TextMeshPro>(); //finds the text prefab text
            QUItextcomponent.text = questionUI; //sets the question ui text to the question ui for this question
            QuesText.tag = questionTag; //assigns the questiontext the questiontag (current question)
            currentQuestion++;  //increments current question counter
        }
        currentQuestion = 0;    //resets currentquestion to 0
        Debug.Log("currentQuestion: " + currentQuestion);   //prints the currentquestion in console
        Debug.Log(currentQuestion.ToString());  //prints the currentquestion as a string (unnecessary, but to see if it is working right)
    }



    public void DisableQuestions()  //function to move question instances into player area from outside render distance
    {         //all instances of question, image, and text are spawned in at start outside of render distance due to issues calling the readjson function more than once
        activeObjects = GameObject.FindGameObjectsWithTag(currentQuestion.ToString());  //finds objects tagged with the current question number and puts them in a string
        foreach(GameObject obj in activeObjects)    //for each object in that string
        {
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y -100.0f, obj.transform.position.z); //translate it down 100.0f on the y axis
        }
        
    }

    public void EndGameCheck() //function to check if the currentquestion is equal to the number of questions
    {                           //currentquestion starts at minus one for assigning jsonobjects to strings so it checks when currentquestion exceeds num of questions
        if (currentQuestion == numOfQuestions)  //if currentquestion is equal to num of quesitons
        {
            string finalScore = score.ToString();   //puts final score int into a string
            Debug.Log(score + "/" + numOfQuestions);    //prints final score in console
            showUI.UION();  //runs turn on ui function from show UI script
        }
    }

    IEnumerator AddImage(Vector3 spawnPosition, string questionTag) //new ienumerateor with position vector and questiontag fed into it
    {
        switch (txtcounter) //switch statement for instances of answers from txtcounter
        {   
            case 0: //if txtcounter = 0
                //textComponent.text = correctAnswer; //assigns correct answer to the textcomponent of the answer text
                downloadedImage.imageToLoad = correctAnswer; //assigns the correct answer image to the image to load
                break; //breaks from switch statement
            case 1: //if txtcounter = 1
                //textComponent.text = wrongAnswer1;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer1; //assigns the wronganswer1 image to the image to load
                break; //breaks from switch statement
            case 2: //if txtcounter = 2
                //textComponent.text = wrongAnswer2;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer2; //assigns the wronganswer2 image to the image to load
                break; //breaks from switch statement
            case 3: //if txtcounter = 3
                //textComponent.text = wrongAnswer3;  //assigns wrong answer
                downloadedImage.imageToLoad = wrongAnswer3; //assigns the wronganswer3 image to the image to load
                break; //breaks from switch statement
        }
        yield return StartCoroutine(downloadedImage.LoadImage(downloadedImage.imageToLoad)); //waits until the loadimage coroutine in the downloadimage script
        Texture tex = downloadedImage.texture; //creates a new texture from the texture created in downloadimage script
        if (imgcounter >= answersInstances.Length)  //if the imagecounter is greater than the number of answers
        {
            imgcounter = 0; //imagecounter set to 0
        }
        position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 4.0f)  + 100.0f, Random.Range(-4.0f, 4.0f)); //creates new vector with random range in parameters
        Debug.Log("position used: " + position); //prints the new position in console (for debugging)
        GameObject questionImage = Instantiate (imageInstances[imgcounter], spawnPosition, Random.rotation); //instantiates a new image object (cube to put image on)
        questionImage.tag = questionTag; //puts a tag on the new image object
        yield return new WaitForSeconds(2);       //wait 2 seconds (for material)
        Renderer rend = questionImage.GetComponent<Renderer>(); //get meshrenderer component from the image cube
        rend.material = new Material(rend.material); //creates a new material
        rend.material.SetTexture("_BaseMap",tex); //assigns the downloaded image to the material
        Debug.Log("Loaded Material"); //prints loaded material (for debugging)
        //Debug.Log(textComponent.text); //prints the textcomponent text in console (for debugging)
        imagesLoaded--; //decrements from the imagesloaded
        if (imagesLoaded == 0) //when the imagesloaded = 0
        {
            DisableQuestions(); //run the disable questions function (cycles questions(should rename))
        }
    }
}
