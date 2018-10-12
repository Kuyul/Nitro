using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public static GameControl Instance;

    //Declare public variables
    public Text ScoreText;
    public GameObject patternSpawnPt;
    public GameObject[] patternPool;

    public GameObject trail;
    public GameObject trailCentre;
    public GameObject trailNitro;
    public GameObject platformSmall;
    public GameObject platformLarge;

    public bool Nitro = false;

    //Gameobjects Controllers
    public PlayerController Player; //Player
    public DeathpoolController DeathPool; //Deathpool

    //Declare private variables
    private int CurrentScore = 0;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Globally initialise to non-nitro mode
        DeactivateNitro();
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        ScoreText.text = "Score: " + CurrentScore;
    }

    //Global Nitro Activation
    public void ActivateNitro()
    {
        Nitro = true;
        Player.ActivateNitro();
        DeathPool.ActivateNitro();
    }

    //Global Nitro Deactivation
    public void DeactivateNitro()
    {
        Nitro = false;
        Player.DeactivateNitro();
        DeathPool.DeactivateNitro();
    }
}
