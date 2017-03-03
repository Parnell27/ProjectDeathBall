using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }
            
        PlayerMovement();
	}

    public float moveSpeed = 8f; //Default movement speed
    
    void PlayerMovement()
    {
        float moving = Input.GetAxis("Vertical") * moveSpeed; 
        //Creates movement in the direction the vertcal and horizontail axes are facing.
        float strafing = Input.GetAxis("Horizontal") * moveSpeed;
        moving *= Time.deltaTime;
        strafing *= Time.deltaTime;
        transform.Translate(strafing, 0, moving);
    }
}
