using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    
    public int currentScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Score()
    {
        currentScore = 0;
    }

   public void UpdateScore()
   {
     currentScore ++;
   }

   
}
