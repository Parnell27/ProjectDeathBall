using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBallPickup2 : NetworkBehaviour {

    public GameObject BallPlaceholder;
    //An empty game object to specify which object is the ball placeholder

    public Transform HoldPosition;

    public GameObject cameraDirection;

    [SyncVar] public bool ballHeld = false;
    float throwSpeed = 12f;
    float timeLastThrown;
    float pickupDelay = 1;

    private void Start()
    {
        timeLastThrown = Time.time;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            MoveWithCamera();
        }
            ThrowBall();
    }


    void MoveWithCamera()
    {
        HoldPosition.position = PlayerCamera.instance.transform.position;
        //Moves the ball position as the position of the camera changes

        HoldPosition.rotation = PlayerCamera.instance.transform.rotation;
        //Rotates the ball as the rotation of the camera changes
    }

    void OnCollisionEnter(Collision touchBall)
    {
        if (touchBall.gameObject == BallScript.singleton.gameObject && Time.time - timeLastThrown >= pickupDelay)
        {
            if (isServer)
            {
                ServerPickupBall(touchBall);
                //Runs the pickup method on the server upon detection of collision between the player and an object

                Debug.Log("Ball picked up");
            }

            if (isLocalPlayer)
            {
                if (startTimer != null)
                {
                    StopCoroutine(startTimer);
                }
                startTimer = StartCoroutine(gameObject.GetComponent<DeathTimer>().TimerCountdown());
                //Runs a coroutine from the death timer that starts the countdown
            }
        }
    }

    Coroutine startTimer;

    void ThrowBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer && ballHeld == true)
        {
            CmdThrowBall(cameraDirection.transform.position, cameraDirection.transform.forward);
            Debug.Log("Ball thrown");
            Debug.Log("Death Timer stopped: " + Time.time);

            timeLastThrown = Time.time;

            if (startTimer != null)
            {
                StopCoroutine(startTimer);
                UIManager.singleton.deathTimerText.text = "5";
                startTimer = null;
            }

            StopCoroutine(gameObject.GetComponent<DeathTimer>().TimerCountdown());
            
        }
    }

    public void DropBall()
    {
        if (ballHeld)
        {
            ServerDropBall();
        }
    }

    [Server] // Specifies code that runs on the server
    void ServerPickupBall(Collision touchBall)
    {
        if (touchBall.gameObject == BallScript.singleton.gameObject)
        {
            BallScript.singleton.gameObject.SetActive(false);
            //Disables the ball object within the scene

            BallPlaceholder.SetActive(true);
            //Enables the ball placeholder for the player holding the ball

            ballHeld = true;

            RpcPickupBall();
        }

        /* The above code detects if the collsion is between the player and the ball object.
           If so, enables the placeholder and disables the ball to appear as if the player
           is now holding the ball. */

    }

    [Command]
    void CmdThrowBall(Vector3 position, Vector3 forward)
    {
        BallScript.singleton.gameObject.SetActive(true);
        BallScript.singleton.gameObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);

        BallScript.singleton.GetComponent<Rigidbody>().velocity = forward * throwSpeed;
        ballHeld = false;

        RpcThrowBall(position, forward);
    }

    void ServerDropBall()
    {
        BallScript.singleton.gameObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);
        BallScript.singleton.gameObject.SetActive(true);

        ballHeld = false;

        RpcDropBall();
    }

    [ClientRpc] // Specifies code that runs on all connected clients
    void RpcPickupBall()
    {
        BallScript.singleton.gameObject.SetActive(false);
        BallPlaceholder.SetActive(true);
    }

    /* The above code perfroms the same function as the  ServerPickupBall function, but executes on the
       player who picked up the ball so that it is visiblle on all connected clients. */

    [ClientRpc]
    void RpcThrowBall(Vector3 position, Vector3 forward)
    {
        BallScript.singleton.gameObject.SetActive(true);
        BallPlaceholder.SetActive(false);

        BallScript.singleton.GetComponent<Rigidbody>().velocity = forward * throwSpeed;
        ballHeld = false;
    }

    void RpcDropBall()
    {
        BallScript.singleton.gameObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);
        BallScript.singleton.gameObject.SetActive(true);
    }
}
