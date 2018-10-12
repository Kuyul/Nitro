using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

    //Declare public variables
    public Rigidbody2D DeathPool;
    public float DeathPoolSpeed = 0.5f;
    public int NoPlatformsForNitro = 5;

    //Declare private variables
    private Vector3 TouchPosition;
    private bool IsDragging;
    private Rigidbody2D rb;
    private int platformsPassed = 0;
    private int platformsDestroyed = 0;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //Drag to move mechanic - logs the initial touch position and move the ball accordingly
        if (Input.GetMouseButtonDown(0))
        {
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IsDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }

        if (IsDragging)
        {
            //Drag Left and Right to move the ball
            float moveAmount = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - TouchPosition.x;
            Vector2 newPosition = new Vector2(moveAmount, 0);
            Vector2 targetPosition = rb.position + newPosition;
            transform.position = targetPosition;
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //On colliding on a platform, perform differing function depending on its Nitro status
        if (collision.tag == "Platform")
        {
            //If Nitro mode, bonus score is added for consecutive platforms broken, +1 -> +2 -> +3
            if (GameControl.Instance.Nitro)
            {
                collision.gameObject.SetActive(false);
                platformsDestroyed++;
                GameControl.Instance.AddScore(platformsDestroyed);
            }
            //If Nitro mode is inactive, game over
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        //On passing a platform, perform differing functions depending on its Nitro status
        if(collision.tag == "Pass")
        {
            GameControl.Instance.AddScore(1);
            //When Nitro mode
            if (GameControl.Instance.Nitro)
            {
                UpdateNitro(false);
            }
            else
            //During normal mode, fill up nitro gauge, and set nitro mode to active when it passes the nitro threshold
            {
                platformsPassed++;
                if (platformsPassed >= NoPlatformsForNitro)
                {
                    UpdateNitro(true);
                }
            }
        }

        //Generate next pattern
        if (collision.tag == "Pattern")
        {            
            Instantiate(GameControl.Instance.patternPool[Random.Range(0, GameControl.Instance.patternPool.Length)], GameControl.Instance.patternSpawnPt.transform.position, Quaternion.identity);
        }
    }

    private void UpdateNitro(bool nitro)
    {
        //Perform differing functions depending on the updated Nitro status
        if (nitro == true) {
            GameControl.Instance.ActivateNitro();
            this.ActivateNitro();
        }
        else
        {
            GameControl.Instance.DeactivateNitro();
            this.DeactivateNitro();
        }
    }

    //TODO: only the colours change for now, but later we'll make it a sprite change or something
    public void ActivateNitro()
    {
        sr.color = Color.red;
        platformsPassed = 0; //Reset platforms passed count
    }

    public void DeactivateNitro()
    {
        sr.color = Color.white;
        platformsDestroyed = 0; //Reset platforms destroyed count
    }
}
