using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float InitialSpeed = 3.0f;
    public float NitroSpeed = 5.0f;

    //Declare Private variables
    private float CurrentSpeed = 3.0f;
    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ActivateNitro()
    {
        SetSpeed(NitroSpeed);
    }

    public void DeactivateNitro()
    {
        SetSpeed(InitialSpeed);
    }

    public float GetCurrentSpeed()
    {
        return CurrentSpeed;
    }

    private void SetSpeed(float newSpeed)
    {
        CurrentSpeed = newSpeed;
        Vector3 velocity = new Vector3(0, newSpeed);
        rb.velocity = velocity;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
