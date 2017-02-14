using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float moveSpeed = 8f; //Default movement speed
    
    void PlayerMovement()
    {
        //Change float value to get key functions for wasd controls

        float moving = Input.GetAxis("Vertical") * moveSpeed; 
        //Creates movement in the direction the vertcal and horizontail axes are facing.
        float strafing = Input.GetAxis("Horizontal") * moveSpeed;
        moving *= Time.deltaTime;
        strafing *= Time.deltaTime;
    }
}
