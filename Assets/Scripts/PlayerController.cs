using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Declare Public variables
    public float speed = 3.0f;

    //Declare Private variables
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        Vector3 velocity = new Vector3(0, speed);
        rb.velocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
