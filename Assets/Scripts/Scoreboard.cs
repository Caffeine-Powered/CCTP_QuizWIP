using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Scoreboard : MonoBehaviour
{
    public GetData getdata;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getdata = FindObjectOfType<GetData>(); //finds the getdata script
    }
    // Update is called once per frame
    public void UpdateScore()
    {
        getdata.score++; //increments the score int in getdata
    }
}
