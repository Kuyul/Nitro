using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;


public class GameControl : MonoBehaviour
{

    public static GameControl Instance;

    //Declare public variables
    public Text textCurrentScore;
    public Text textHighScore;
    public TextMesh NitroText;
    public float MaxNitro = 3.0f;

    public GameObject trail;
    public GameObject trailCentre;
    public GameObject trailNitro;
    public GameObject platformSmall;
    public GameObject platformLarge;
    public GameObject death;
    public GameObject goaleffecttop;
    public GameObject goaleffectbottom;
    public GameObject ball;
    public GameObject trailSide1;
    public GameObject trailSide2;
    public GameObject Taptoplay;
    public GameObject highScore;


    public bool Nitro = false;

    //Gameobjects Controllers
    public PlayerController Player; //Player
    public DeathpoolController DeathPool; //Deathpool
    public BallController Ball; //Ball
    public LevelController Level; //Level Generator

    //Declare private variables
    private int CurrentScore = 0;
    private float NitrotimeLeft = 0;
    private bool InitialMouseClick = false;

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 300;
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
        Time.timeScale = 0;

        textHighScore.text = "Best Score : " + PlayerPrefs.GetInt("highscore", 0);
    }

    //While Nitro is active, countdown nitro time until nitrotimeleft is less than 0, then deactivate nitro
    private void Update()
    {
        if (!InitialMouseClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameAnalytics.Initialize();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
                Time.timeScale = 1;
                InitialMouseClick = true;
                Taptoplay.SetActive(false);
                highScore.SetActive(false);
            }
        }

       if (NitrotimeLeft > 0)
        {
            NitrotimeLeft -= Time.deltaTime;
            if (NitrotimeLeft <= 0)
            {
                NitrotimeLeft = 0;
                DeactivateNitro();
                Ball.DeactivateNitro();
            }
            NitroText.text = NitrotimeLeft.ToString("#.#");
        }
    }

    public void AddScore(int score)
    {
        CurrentScore += score * Level.GetCurrentLevel();
        textCurrentScore.text = CurrentScore.ToString();

        if(CurrentScore>PlayerPrefs.GetInt("highscore",0))
        {
            PlayerPrefs.SetInt("highscore", CurrentScore);
        }
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
            NitrotimeLeft = MaxNitro;
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

    //Call level controller to set new level
    public void LevelComplete()
    {

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", CurrentScore);

        Level.IncrementLevel();
        StartCoroutine("timer");
    }

    public Vector3 GetPlayerPosition()
    {
        return Player.GetCurrentPosition();
    }

    public void Die()
    {   
        Instantiate(death, ball.transform.position, Quaternion.identity);
        ball.SetActive(false);
        Player.SetSpeed(0);
        StartCoroutine("timer");
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
