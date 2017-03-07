using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;
        //locks the cursor so it doesn't appear in-game

	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //unlocks the cursor upon pressing the escape key
    }
}
