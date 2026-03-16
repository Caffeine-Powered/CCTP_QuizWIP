using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;

public class GetData : MonoBehaviour
{
    public string DataURL;
    public int numOfQuestions;
    public QuestionSetup questionSetup;
    public AnswerButton answerButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                //JsonData = json; 
                ReadJSON(json);


                questionSetup.GetQuestionAssets();
                questionSetup.Cycle();
            }
        }
    }

    void ReadJSON(string jsonString) //this inside of questiondata
    {
        JSONNode node = JSON.Parse(jsonString);
        JSONObject obj = node.AsObject;
    
    /**    numOfQuestions = (obj["Questions"].Count);
        


        for (int i = 0 ; i < numOfQuestions; i++)
        {
            Debug.Log(i);

        }
        

        //Debug.Log(obj["Questions"][0]);
        //Debug.Log(obj["Questions"][1]);
        //Debug.Log(obj["Questions"][2]);

    **/
     }
}
