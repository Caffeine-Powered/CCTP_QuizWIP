using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RandomSpawn : MonoBehaviour
{
 public GameObject spawnObj;

 public float MinX = 0;
 public float MaxX = 10;
 public float MinY = 0;
 public float MaxY = 10;
 public float MinZ = 0;
 public float MaxZ = 10;


void Start()
{
    float x = Random.Range(MinX,MaxX);
    float y = Random.Range(MinY,MaxY);
    float z = Random.Range(MinZ,MaxZ);

    Vector3 spawnPosition = new Vector3(x,y,z);
    spawnObj.transform.position = spawnPosition;
}

public void OnClick()
{
    Debug.Log("MOVED");
    Start();
}

}
