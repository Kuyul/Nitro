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
    public Slider NitroGauge;
    public float MaxNitro = 3.0f;

    public GameObject trail;
    public GameObject trailCentre;
    public GameObject trailNitro;
    public GameObject platformSmall;
    public GameObject platformLarge;
    public GameObject trailSide1;
    public GameObject trailSide2;

    public bool Nitro = false;

    //Gameobjects Controllers
    public PlayerController Player; //Player
    public DeathpoolController DeathPool; //Deathpool
    public BallController Ball; //Ball
    public LevelController Level; //Level Generator

    //Declare private variables
    private int CurrentScore = 0;
    private float NitrotimeLeft = 0;

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

    //While Nitro is active, countdown nitro time until nitrotimeleft is less than 0, then deactivate nitro
    private void Update()
    {
        if (NitrotimeLeft > 0)
        {
            Debug.Log(NitrotimeLeft);
            Debug.Log(Time.deltaTime);
            NitrotimeLeft -= Time.deltaTime;
            if (NitrotimeLeft <= 0)
            {
                NitrotimeLeft = 0;
                DeactivateNitro();
                Ball.DeactivateNitro();
            }
        }

        NitroGauge.value = NitrotimeLeft / MaxNitro;
        Debug.Log(NitrotimeLeft / MaxNitro);
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

    public void AddNitroTime(float time)
    {
        NitrotimeLeft += time;
        if(NitrotimeLeft >= MaxNitro)
        {
            NitrotimeLeft = 3.0f;
        }
    }

    public void ActivateTrail(bool input)
    {
        trailCentre.SetActive(input);
        trailNitro.SetActive(input);
        trail.SetActive(!input);
        trailSide1.SetActive(input);
        trailSide2.SetActive(input);
    }
}
