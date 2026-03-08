using UnityEngine;

public class QuestionDataVariable : MonoBehaviour
{
    public string question;
    public string category;
    [Tooltip("The correct answer should always be listed first, they are ranzomized later")]
    public string correctAnswer;
    public string wrongAnswer1;
    public string wrongAnswer2;
    public string wrongAnswer3;
}
