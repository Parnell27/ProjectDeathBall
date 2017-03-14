using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public static PlayerController localPlayer;
    /*Creates an instance of the player controller called localPlayer which specifies
    the player whom the script is running for */

    public new Camera camera;
    //Creates an instance of a camera under the name of camera that will store the player camera


	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            Destroy(camera.gameObject);
            Destroy(this);
            /* Destroys additional instances of player cameras and players from the player
            controllers prefabif they aren't the local player when they spawm. This means only
            a player and a player camera will spawn when a player joins, instead of any repeated
            objects.*/
        }
        else
        {
            localPlayer = this;
            camera.gameObject.AddComponent<PlayerCamera>();
            gameObject.AddComponent<PlayerMovement>();
            /*If the object spawned is the local player on that instance of the game
            the camera and movement scripts are added to the new player upon spawning. */
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        //Locks the cursor so it doesn't appear in-game

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //Unlocks the cursor upon pressing the escape key
    }
}
