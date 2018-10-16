using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

    //Declare public variables
    public Rigidbody2D DeathPool;
    public float DeathPoolSpeed = 0.5f;
    public float Boundary = 1.6f;

    //Declare private variables
    private Vector3 TouchPosition;
    private bool IsDragging;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
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
            //Check whether new position of the ball is within the boundary, if not, don't move
            if(Mathf.Abs(targetPosition.x) < Boundary)
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
                Vector2 localPosition = new Vector2 (transform.position.x, transform.position.y+0.3f);
                collision.gameObject.SetActive(false);
                GameObject platformS = Instantiate(GameControl.Instance.platformSmall, localPosition, Quaternion.identity);
                Destroy(platformS, 2);
                GameObject platformL = Instantiate(GameControl.Instance.platformLarge, localPosition, Quaternion.identity);
                Destroy(platformL, 2);
                GameControl.Instance.AddScore(1);
            }
            //If Nitro mode is inactive, game over
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        //On passing a platform, perform differing functions depending on its Nitro status
        /* Depreciated
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
        }*/

        if (collision.tag == "NitroBlock")
        {
            collision.gameObject.SetActive(false);
            GameControl.Instance.AddNitroTime(0.5f);
            if (!GameControl.Instance.Nitro)
            {
                UpdateNitro(true);
            }
        }

        //Die on Deathpool
        if (collision.tag == "Deathpool")
        {
            SceneManager.LoadScene(0);
        }

        //Goal
        if (collision.tag == "Goal")
        {
            //TODO: Should be gameEnd
            SceneManager.LoadScene(0);
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
        GameControl.Instance.ActivateTrail(true);
        transform.localScale = new Vector2(2.0f, 2.0f);
    }

    public void DeactivateNitro()
    {
        GameControl.Instance.ActivateTrail(false);
        transform.localScale = new Vector2(1.0f, 1.0f);
    }
}
