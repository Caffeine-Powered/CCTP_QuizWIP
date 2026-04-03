using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public GameObject[] cObject;
    public GameObject[] objects;
    public void OnClick()
    {
        objects = GameObject.FindGameObjectsWithTag("QuestionBox");
            foreach(GameObject obj in objects)
            {
                Destroy(obj);
            }
        cObject = GameObject.FindGameObjectsWithTag("CorrectAnswer");
            foreach(GameObject obj in cObject)
            {
                Destroy(obj);
            }
        

    }
}
