using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: needs major reforming - Level design not complete
public class LevelController : MonoBehaviour {

    //Declare public variables
    public Text textCurrentLevel;
    public Text textNextLevel;
    public GameObject[] Levels;
    public Slider LevelProgress;
    public Sprite[] sprites;
    public SpriteRenderer Background;

    //Declare private variables
    private int CurrentLevel;

    private void Start()
    {
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", 1);

        GenerateLevel();

        textCurrentLevel.text = PlayerPrefs.GetInt("currentLevel", 1).ToString();
        textNextLevel.text = (PlayerPrefs.GetInt("currentLevel", 1) + 1).ToString();
        //Random background color on each level
        Background.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update()
    {
        var progress = GameControl.Instance.GetPlayerPosition().y/65.0f;
        LevelProgress.value = progress;
    }

    public void GenerateLevel()
    {
        //Instantiate(Levels[CurrentLevel-1], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void IncrementLevel()
    {
        int level = CurrentLevel;
        level++;
        if (level > Levels.Length)
        {
            level = 1;
        }

        PlayerPrefs.SetInt("currentLevel", level);
    }
}
