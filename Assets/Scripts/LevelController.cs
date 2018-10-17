using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: needs major reforming - Level design not complete
public class LevelController : MonoBehaviour {

    //Declare public variables
    public int HowManyPatterns = 10;
    public Text textCurrentLevel;
    public Text textNextLevel;
    public GameObject[] Patterns;
    public Transform InitialPoint;
    public Slider LevelProgress;
    public Sprite[] sprites;
    public SpriteRenderer Background;
    public GameObject Goal;

    //Declare private variables
    private int CurrentLevel;
    private float Offset;

    private void Start()
    {
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        Offset = 0;

        GenerateLevel();

        textCurrentLevel.text = PlayerPrefs.GetInt("currentLevel", 1).ToString();
        textNextLevel.text = (PlayerPrefs.GetInt("currentLevel", 1) + 1).ToString();
        //Random background color on each level
        Background.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update()
    {
        var progress = GameControl.Instance.GetPlayerPosition().y/Offset;
        LevelProgress.value = progress;
    }

    public void GenerateLevel()
    {
        for(int i = 0; i < HowManyPatterns; i++)
        {
            //There are two indicators that indicate bottom and top level of the pattern.
            //We're going to first find the localposition of the bottom and add it to the offset
            GameObject p = Patterns[Random.Range(0, Patterns.Length)];
            PatternScript ps = p.GetComponent<PatternScript>();
            Offset += Mathf.Abs(ps.GetBottomPositionOffset().y); //add bottom offset
            Vector3 offsetVector = new Vector3(0, Offset, 0);
            Instantiate(p, InitialPoint.position + offsetVector, Quaternion.identity);
            Offset += ps.GetTopPositionOffset().y; //add top offset
            //Give 2 extra units of offset
            Offset += 2.0f;
        }
        //Little bit of extra offset
        Offset += 3.0f;
        Vector3 goalPos = new Vector3(0, Offset, 0);
        Instantiate(Goal, goalPos, Quaternion.identity);
    }

    public void IncrementLevel()
    {
        int level = CurrentLevel;
        level++;
        PlayerPrefs.SetInt("currentLevel", level);
    }

    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }
}
