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
        PlayerJump();

    }

    public float moveSpeed = 10.0f; //Default movement speed
    public float jumpHeight = 1000.0f; //Default jump height

    void PlayerMove()
    {
        float moving = Input.GetAxis("Vertical") * moveSpeed;
        //Creates movement in the direction the vertcal and horizontail axes are facing.
        float strafing = Input.GetAxis("Horizontal") * moveSpeed;
        moving *= Time.deltaTime;
        strafing *= Time.deltaTime;
        transform.Translate(strafing, 0, moving);

    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1)) //ADD RAYCAST CHECK STUFF HERE
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
            
            /* Adds the value of jumpHeight as a force upwards when the space key is pressed.
            This creates the upward motion of the jump. Gravity and the rigidbdy will control
            bringing the player back down to the ground. */
        }
    }
}
