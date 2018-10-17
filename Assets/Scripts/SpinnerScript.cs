using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {

    public float AngularVel = 0;
    private bool Triggered = false;

    // Use this for initialization
    void Start () {
        //set angular vel
        if (AngularVel != 0)
        {
            GetComponent<Rigidbody2D>().angularVelocity = AngularVel;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
