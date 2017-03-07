using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public static PlayerController localPlayer;
    public Camera camera;


	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            Destroy(camera.gameObject);
            Destroy(this);
        }
        else
        {
            localPlayer = this;
            camera.gameObject.AddComponent<PlayerCamera>();
            gameObject.AddComponent<PlayerMovement>();
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        //locks the cursor so it doesn't appear in-game

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //unlocks the cursor upon pressing the escape key
    }
}
