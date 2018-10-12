using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathpoolController : MonoBehaviour {

    //Initialise Public variables
    private float MoveInAwaySpeed = 0.2f;

    //Initialise Private variables
    private Rigidbody2D rb;
    private Vector2 Velocity;
    private Vector2 InitialPosRelativetoCamera;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        InitialPosRelativetoCamera = GetCameraPosition() - transform.position;
	}

    void FixedUpdate()
    {
        //Calculate new position first
        Vector3 newPosition = rb.position + Velocity * Time.deltaTime;
        Vector3 newPosRelativeToCamera = GetCameraPosition() - newPosition;

        //We don't want the deathpool to drop below the initial position
        //If the new position of the deathpool is further than the initial position (relative to camera, then stop moving)
        if (newPosRelativeToCamera.y <= InitialPosRelativetoCamera.y)
        {
            rb.MovePosition(newPosition);
        }else
        {
            StopMovingOut();
            //Re-calcualte position
            Vector3 stopPosition = rb.position + Velocity * Time.deltaTime;
            rb.MovePosition(stopPosition);
        }
    }

    //Fetch Nitro Speed from Player controller and move away from the ball at a desingated speed
    public void ActivateNitro()
    {
        float newSpeed = GameControl.Instance.Player.NitroSpeed;
        Velocity = new Vector2(0, newSpeed - MoveInAwaySpeed);
    }

    //Fetch Initial Speed from Player controller and move in on the ball at a designated speed
    public void DeactivateNitro()
    {
        float newSpeed = GameControl.Instance.Player.InitialSpeed;
        Velocity = new Vector2(0, newSpeed + MoveInAwaySpeed);
    }

    private void StopMovingOut() { 
        float newSpeed;
        if (GameControl.Instance.Nitro)
        {
            newSpeed = GameControl.Instance.Player.NitroSpeed;
        }
        else
        {
            newSpeed = GameControl.Instance.Player.InitialSpeed + MoveInAwaySpeed;
        }
        Velocity = new Vector2(0, newSpeed);
    }

    private Vector3 GetCameraPosition()
    {
        return Camera.main.gameObject.transform.position;
    }
}
