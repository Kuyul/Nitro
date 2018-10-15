using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    //Declare public variables
    public int NumberOfPlatforms = 30;
    public Transform InitialPos;
    public GameObject[] Platforms;
    public GameObject[] NitroPlatforms;
    public GameObject Goal;
    public float PlatformOffset = 2.0f;

    GameObject NormalPlatformContainer;
    GameObject NitroPlatformContainer;

    private void Start()
    {
        NormalPlatformContainer = new GameObject("Normal Platforms");
        NitroPlatformContainer = new GameObject("Nitro Platforms");
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for(int i = 0; i < NumberOfPlatforms; i++)
        {
            Vector3 newPos = InitialPos.position + new Vector3(0, i * PlatformOffset, 0);
            var newobj = Instantiate(Platforms[Random.Range(0, Platforms.Length)], newPos, Quaternion.identity);
            newobj.transform.SetParent(NormalPlatformContainer.transform);
        }

        /*
        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            Vector3 newPos = InitialPos.position + new Vector3(0, i * PlatformOffset, 0);
            var newobj = Instantiate(NitroPlatforms[Random.Range(0, NitroPlatforms.Length)], newPos, Quaternion.identity);
            newobj.transform.SetParent(NitroPlatformContainer.transform);
        }*/

        Vector3 endPos = InitialPos.position + new Vector3(0, NumberOfPlatforms * PlatformOffset, 0);
        Instantiate(Goal, endPos, Quaternion.identity);
    }

    public void ActivateNitro()
    {
        //NormalPlatformContainer.SetActive(false);
        //NitroPlatformContainer.SetActive(true);
    }

    public void DeactivateNitro()
    {
        NormalPlatformContainer.SetActive(true);
        //NitroPlatformContainer.SetActive(false);
    }
}
