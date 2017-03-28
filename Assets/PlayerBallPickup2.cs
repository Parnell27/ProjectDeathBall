using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBallPickup2 : NetworkBehaviour {

    public GameObject BallPlaceholder;
    //An empty game object to specify which object is the ball placeholder

    void MoveWithCamera()
    {
        BallPlaceholder.transform.position = PlayerCamera.instance.transform.position;
        //Moves the ball position as the position of the camera changes

        BallPlaceholder.transform.rotation = PlayerCamera.instance.transform.rotation;
        //Rotates the ball as the rotation of the camera changes
    }

    void OnCollisionEnter(Collision touchBall)
    {
        ServerPickupBall(touchBall);
        //Runs the pickup method on the server upon detection of collision between the player and an object
    }

    void ThrowBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer && BallSpawn.ballObject.active == false)
        {
            ServerThrowBall();
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
        BallPlaceholder.SetActive(false);

        RpcThrowBall();
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
    }
}
