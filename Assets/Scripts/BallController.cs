using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

    //Declare private variables
    private Vector3 TouchPosition;
    private bool IsDragging;
    private Rigidbody2D rb;
    private int platformsPassed = 0;
    private int platformsDestroyed = 0;
    private bool Nitro = false;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Nitro)
        {

        }

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
        if (collision.tag == "Platform")
        {
            if (Nitro)
            {
                collision.gameObject.SetActive(false);
                platformsDestroyed++;
                GameControl.Instance.AddScore(platformsDestroyed);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        if(collision.tag == "Pass")
        {
            if (Nitro)
            {
            }
            else
            {
                GameControl.Instance.AddScore(1);
                platformsPassed++;
                if (platformsPassed >= 5)
                {
                    SetNitro(true);
                }
            }
        }
    }

    private void SetNitro(bool nitro)
    {
        Nitro = nitro;
        if (nitro == true) {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }

    }
}
