using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //Declare public variables
    public int NumberOfPlatforms = 30;
    public Transform InitialPos;
    public GameObject[] Platforms;
    public float PlatformOffset = 2.0f;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for(int i = 0; i < NumberOfPlatforms; i++)
        {
            Vector3 newPos = InitialPos.position + new Vector3(0, i * PlatformOffset, 0);
            Instantiate(Platforms[Random.Range(0, Platforms.Length)], newPos, Quaternion.identity);
        }
    }
}
