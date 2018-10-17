using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public FollowPath[] Triggerobjs;
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Triggerobjs.Length; i++)
        {
            if (collision.tag == "Ball")
            {
                Triggerobjs[i].SetTrigger();
            }
        }
    }
}
