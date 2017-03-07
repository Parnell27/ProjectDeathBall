using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        PlayerMove();

    }

    public float moveSpeed = 10.0f; //Default movement speed

    void PlayerMove()
    {
        float moving = Input.GetAxis("Vertical") * moveSpeed;
        //Creates movement in the direction the vertcal and horizontail axes are facing.
        float strafing = Input.GetAxis("Horizontal") * moveSpeed;
        moving *= Time.deltaTime;
        strafing *= Time.deltaTime;
        transform.Translate(strafing, 0, moving);

    }
}
