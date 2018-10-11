using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl Instance;

    //Declare public variables
    public Text ScoreText;

    //Declare private variables
    private int CurrentScore = 0;

	// Use this for initialization
	void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update () {
		
	}

    public void AddScore(int score)
    {
        CurrentScore += score;
        ScoreText.text = "Score: " + CurrentScore;
    }
}
