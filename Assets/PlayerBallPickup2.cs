using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBallPickup2 : NetworkBehaviour {

    public GameObject BallPlaceholder;
    //An empty game object to specify which object is the ball placeholder

    public Transform HoldPosition;

    public GameObject cameraDirection;

    public  bool ballHeld = false;
    float throwSpeed = 12f;
    float timeLastThrown;
    float pickupDelay = 1;

    private void Start()
    {
        timeLastThrown = Time.time;
    }

    private void Update()
    {
            MoveWithCamera();
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
        if (touchBall.gameObject == BallSpawn.ballObject && Time.time - timeLastThrown >= pickupDelay)
        {
            ServerPickupBall(touchBall);
            //Runs the pickup method on the server upon detection of collision between the player and an object

            Debug.Log("Ball picked up");
            ballHeld = true;

            StartCoroutine(gameObject.GetComponent<DeathTimer>().StartTimer());
            //Runs a coroutine from the death timer that starts the countdown

        }
    }

    void ThrowBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer)
        {
            if (ballHeld == true)
            {
                ServerThrowBall();
                Debug.Log("Ball thrown");
                Debug.Log("Death Timer stopped: " + Time.time);

                ballHeld = false;
                timeLastThrown = Time.time;

                StopCoroutine(gameObject.GetComponent<DeathTimer>().StartTimer());
            }
        }
    }

    public void DropBall()
    {
        if (ballHeld)
        {
            ServerDropBall();
            ballHeld = false;
        }
    }

    [Server] // Specifies code that runs on the server
    void ServerPickupBall(Collision touchBall)
    {
        if (touchBall.gameObject == BallSpawn.ballObject)
        {
            BallSpawn.ballObject.SetActive(false);
            //Disables the ball object within the scene

            BallPlaceholder.SetActive(true);
            //Enables the ball placeholder for the player holding the ball

            RpcPickupBall();
        }

        /* The above code detects if the collsion is between the player and the ball object.
           If so, enables the placeholder and disables the ball to appear as if the player
           is now holding the ball. */

    }

    void ServerThrowBall()
    {
        BallSpawn.ballObject.SetActive(true);
        BallSpawn.ballObject.transform.position = BallPlaceholder.transform.position;
        

        BallPlaceholder.SetActive(false);

        //BallSpawn.ballObject.GetComponent<Rigidbody>().AddForce(cameraDirection.transform.forward * throwSpeed);
        BallSpawn.ballObject.GetComponent<Rigidbody>().velocity = cameraDirection.transform.forward * throwSpeed;

        RpcThrowBall();
    }

    void ServerDropBall()
    {
        BallSpawn.ballObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);
        BallSpawn.ballObject.SetActive(true);

        RpcDropBall();
    }

    [ClientRpc] // Specifies code that runs on all connected clients
    void RpcPickupBall()
    {
        BallSpawn.ballObject.SetActive(false);
        BallPlaceholder.SetActive(true);
    }

    /* The above code perfroms the same function as the  ServerPickupBall function, but executes on the
       player who picked up the ball so that it is visiblle on all connected clients. */

    void RpcThrowBall()
    {
        BallSpawn.ballObject.SetActive(true);
        BallSpawn.ballObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);
        BallSpawn.ballObject.GetComponent<Rigidbody>().velocity = cameraDirection.transform.forward * throwSpeed;
    }

    void RpcDropBall()
    {
        BallSpawn.ballObject.transform.position = BallPlaceholder.transform.position;
        BallPlaceholder.SetActive(false);
        BallSpawn.ballObject.SetActive(true);
    }
}
