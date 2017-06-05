using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBallPickup2 : NetworkBehaviour {

    public GameObject BallPlaceholder;  //An empty game object to specify which object is the ball placeholder
    public Transform HoldPosition;      //An empty transform to contain the holding position on the player
    public GameObject BallSpawner;      //An empty game object to specify which objetc is the ball spawner
    public GameObject cameraDirection;  //An empty game object to specify which objetc is the camera direction

    public Vector3 currentPos;          //An empty Vector3 to store current position

    [SyncVar] public bool ballHeld = false;     //A synchronised boolean to specify if the ball is held or not
    [SyncVar] double timeLastThrown;            //A synchronised variabke containing the time in the server, the ball was last thrown
    public bool correctBallPos = false;         //A boolean indicating if the ball is in the correct position
    float throwSpeed = 12f;                     //A variable for the player's throwing speed
    float pickupDelay = 1;                      //A variable for the delay before picking up the ball again
    

    private void Start()
    {
        timeLastThrown = Network.time; //Assigns a default value when the match starts so players may pick up the ball
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            MoveWithCamera(); //Calls the MoveWithCamera method each frame if this script is running on the local player
        }
            ThrowBall(); // Runs the ThrowBall method each frame
    }


    //Controls how the ball placeholder moves with the camera
    void MoveWithCamera()
    {
        HoldPosition.position = PlayerCamera.instance.transform.position;
        //Moves the ball position as the position of the camera changes

        HoldPosition.rotation = PlayerCamera.instance.transform.rotation;
        //Rotates the ball as the rotation of the camera changes
    }

    //Controls the code that runs when the player colldes with the ball, defines this collision as touchball
    void OnCollisionEnter(Collision touchBall)
    {
        //Runs the following code if the collision is with the ball object
        if (touchBall.gameObject == BallScript.singleton.gameObject)
        {
            //Runs the following code if this script is running on the server
            if (isServer)
            {
                ServerPickupBall(touchBall);
                //Runs the pickup method on the server upon detection of collision between the player and an object

                Debug.Log("Ball picked up"); //Logs the pickup of the ball to the console
            }

            //Runs the following code if this script is running on the local player
            if (isLocalPlayer)
            {
                //Runs the following code if the coroutine startTimer has a value
                if (startTimer != null)
                {
                    StopCoroutine(startTimer); //Stops the active coroutine startTimer
                }
                startTimer = StartCoroutine(gameObject.GetComponent<DeathTimer>().TimerCountdown());
                //Runs a coroutine from the death timer that starts the countdown
            }
        }
    }

    Coroutine startTimer; //An empty coroutine for storing the active coroutine from death timer

    //Handles what happens when the ball is thrown
    void ThrowBall()
    {
        //Runs the following code when inout from the left mouse button is detected and the ball is held and the player is the local player
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer && ballHeld == true)
        {
            CmdThrowBall(cameraDirection.transform.position, cameraDirection.transform.forward);
            //Calls the CmdThrowBall method with the position and rotation of the game object camera direction
            Debug.Log("Ball thrown"); //Logs the ball throw to the console
            Debug.Log("Death Timer stopped: " + Network.time); //Logs the stop of the detah timer to console and the time it stopped

            //Runs the following code when start timer isn't empty
            if (startTimer != null)
            {
                StopCoroutine(startTimer); //Stops the startTimer coroutine running
                UIManager.singleton.deathTimerText.text = "5"; //Sets the death timer's Ui element to the default value of 5
                startTimer = null; //Changes the value of startTimer to null
            }

            StopCoroutine(gameObject.GetComponent<DeathTimer>().TimerCountdown());
            //Stops the TimerCountdown coroutine running therefore stopping the death timer
        }
    }

    //Controls how the ball is dropped
    public void DropBall()
    {
        //Runs the following code when the ball is held
        if (ballHeld)
        {
            CmdDropBall(); //Calls the CmdDropBall method
        }
    }

    [Server] // Specifies code that runs on the server
    //Handles how the ball is picked uo on the server
    void ServerPickupBall(Collision touchBall)
    {
        //Runs the following code when the collsion tocuhball is between the player and ball, and the current time exceeds the pickup delay
        if (touchBall.gameObject == BallScript.singleton.gameObject && Network.time - timeLastThrown >= pickupDelay)
        {
            BallScript.singleton.gameObject.SetActive(false);
            //Disables the ball object within the scene

            BallPlaceholder.SetActive(true);
            //Enables the ball placeholder for the player holding the ball

            ballHeld = true; //Sets ballHeld to true

            RpcPickupBall(); //Calls RpcPickupBall
        }

        /* The above code detects if the collsion is between the player and the ball object.
           If so, enables the placeholder and disables the ball to appear as if the player
           is now holding the ball. */

    }

    [Command] //Specifies code that the client is requesting the server runs
    //Handles how the all is thrown on the server
    void CmdThrowBall(Vector3 position, Vector3 forward)
    {
        BallScript.singleton.gameObject.SetActive(true); //Activates the ball object when it is thrown
        BallScript.singleton.gameObject.transform.position = BallPlaceholder.transform.position;
        //Sets the postion to the ball to the current position of the placeholder
        BallPlaceholder.SetActive(false); //Deactivates the placeholder when thrown

        BallScript.singleton.GetComponent<Rigidbody>().velocity = forward * throwSpeed;
        //Applies velocity of throwSpeed in the direction the camera is facing to the ball's rigidbody

        ballHeld = false; //Sets the value of ballHeld to false
        timeLastThrown = Network.time; //Assigns the current time as the value for timeLastThrown as the ball has just been thrown

        RpcThrowBall(position, forward); //Calls RpcThrowBall
    }

    [Command]
    //Handles how the ball is dropped on the server
    void CmdDropBall()
    {
        currentPos = gameObject.transform.position; //Sets currentPos to the current position of the player
        BallScript.singleton.gameObject.SetActive(true); //Activates the ball object
        timeLastThrown = Network.time; //Assigns the current time as the value for timeLastThrown
        BallPlaceholder.SetActive(false); //Deactivates the ball placeholder
        BallScript.singleton.gameObject.transform.position = currentPos; //Sets the position of the ball to the same position as the player

        ballHeld = false; //Sets the vlaue of ballHeld to false

        RpcDropBall(); //Calls RpcDropBall
    }

    [ClientRpc] // Specifies code that runs on all connected clients
    //Handles how the ball is picked up across all clients
    void RpcPickupBall()
    {
        BallScript.singleton.gameObject.SetActive(false); //Deactivates the ball object
        BallPlaceholder.SetActive(true); //Actovates the ball placeholder object
    }

    /* The above code perfroms the same function as the  ServerPickupBall function, but executes on the
       player who picked up the ball so that it is visiblle on all connected clients. */

    [ClientRpc]
    //Handles how the ball is thrown across all clients
    void RpcThrowBall(Vector3 position, Vector3 forward)
    {
        BallScript.singleton.gameObject.SetActive(true); //Activates the ball object
        BallPlaceholder.SetActive(false); //Deactivates the ball placeholder

        BallScript.singleton.GetComponent<Rigidbody>().velocity = forward * throwSpeed;
        //Applies velocity of throwSpeed in the direction the camera is facing to the ball's rigidbody

        ballHeld = false; //Sets the vakue of ballHeld to false
        timeLastThrown = Network.time; //Assigns the current time as the value for timeLastThrown as the ball has just been thrown
    }

    [ClientRpc]
    //Handles how the ball is dropped across all clients
    void RpcDropBall()
    {
        BallScript.singleton.gameObject.SetActive(true); //Activates the ball object
        timeLastThrown = Network.time; //Assigns the current time as the value for timeLastThrown
        BallPlaceholder.SetActive(false); //Deactivates the ball placeholder
        BallScript.singleton.gameObject.transform.position = currentPos; //Sets the position of the ball to the current position of the player
    }
}
