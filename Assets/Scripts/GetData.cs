using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;

public class GetData : MonoBehaviour
{
    public string DataURL;
    public int numOfQuestions;
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(getData());
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
        Debug.Log("Number of Questions " + numOfQuestions);

        for (var i = 0; i < numOfQuestions; i ++)
        {
            Instantiate(prefab, new Vector3(0,0,0),Quaternion.identity);
        }
        Debug.Log("Spawned: " + numOfQuestions +" models");
        //Debug.Log(obj["Questions"].Count);
        //var newQuestion = (obj["Questions"][0]["Question"]);
        //Debug.Log(newQuestion);
     }
}
