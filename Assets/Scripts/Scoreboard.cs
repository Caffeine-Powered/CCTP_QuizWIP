using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    
    public float currentScore;
    public AudioSource CorrectDing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Score()
    {
        currentScore = 0;
    }

   public void UpdateScore()
   {
     currentScore ++;
     Debug.Log("SCORE: "+ currentScore);
     CorrectDing.Play();
     
   }

   
}
