using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        PlayerMove(); //Calls the PlayerMove method each frame
        PlayerJump(); //Calls the PlayerJump method each frame

    }

    public float moveSpeed = 10.0f; //Default movement speed
    public float jumpHeight = 1500.0f; //Default jump height

    void PlayerMove()
    {
        float moving = Input.GetAxis("Vertical") * moveSpeed;
        float strafing = Input.GetAxis("Horizontal") * moveSpeed;
        /*Creates movement in the direction the vertcal and horizontal axes are facing by
        multiplying the position on each axis by the predefined move speed */

        moving *= Time.deltaTime;
        strafing *= Time.deltaTime;
        /*Multiplies the values for moving and strafing by the time so they move in time
         with each update */

        transform.Translate(strafing, 0, moving);
        //Moves the position of the player by the values specified in moving and strafing

    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
            
            /* Checks to see if the space key has been pressed and uses a raycast to check the
            distance between the player and the ground. 
            
            If it is any greater than 0, the player is airrborne and not allowed to jump
            again yet. 
            
            If the player is on the ground adds the value of jumpHeight as a force upwards to
            the player. This creates the upward motion of the jump. Gravity and the rigidbdy will
            control bringing the player back down to the ground. */
        }
    }
}
