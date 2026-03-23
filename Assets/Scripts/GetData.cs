using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GetData : MonoBehaviour
{
    public string DataURL;
    public int numOfQuestions;
    public GameObject correctQText;
    public GameObject[] questionInstances;
    private int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 0;
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

    void ReadJSON(string jsonString) 
    {
        JSONNode node = JSON.Parse(jsonString);
        JSONObject obj = node.AsObject;
    
        numOfQuestions = (obj["Questions"].Count);

        for (int i = 0 ; i < numOfQuestions; i++)
        {
            //Debug.Log(i);
  
            Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(0.0f, 3.0f), Random.Range(-3.0f, 3.0f));
            if (counter >= questionInstances.Length)
            {
                counter = 0;
            }
            Instantiate (questionInstances[counter], position, Random.rotation);
            counter++;

            string questionName = obj["Questions"][i]["Question"].Value;
            string category = obj["Questions"][i]["Category"].Value;
            string correctAnswer = obj["Questions"][i]["Correct Answer"].Value;
            //string wrongAnswer1 = obj["Questions"][i]["Incorrect Answer 1"].Value;
            //string wrongAnswer2 = obj["Questions"][i]["Incorrect Answer 2"].Value;
            //string wrongAnswer3 = obj["Questions"][i]["Incorrect Answer 3"].Value;
            //Debug.Log("correct + " +correctAnswer);
            Debug.Log(correctAnswer);
           

            GameObject myText = Instantiate(correctQText, position, Quaternion.identity);
            TextMeshPro textComponent = myText.GetComponent<TextMeshPro>();
            textComponent.text = correctAnswer;
            

        }
     }
}
