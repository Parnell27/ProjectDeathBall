using UnityEngine;
using UnityEngine.Networking;

public class PlayerBallPickup : NetworkBehaviour {

    public Collider ball;
    //A collider that will be used to store an object's collider

    public GameObject HoldingPosition;

    void OnCollisionEnter(Collision touchBall)
    {
        if (isLocalPlayer)
        {
            HoldingPosition.transform.position = PlayerCamera.instance.transform.position;
            HoldingPosition.transform.rotation = PlayerCamera.instance.transform.rotation;
        }
        ServerPickupBall(touchBall);
        /* Upon colliding an object the collision touchBall is created. If the game object
        collided with is the ball object that was spawned (that will be stored in the collider)
        the ball will be picked up.*/
        
    }

    [Server] // Specifies code that runs on the server
    void ServerPickupBall(Collision touchBall)
    {
        if (touchBall.gameObject == BallSpawn.ballObject)
        {
            //Debug.Log("Ball touched");
            touchBall.transform.SetParent(HoldingPosition.transform, true);
            /* Sets the holding position as the parent of the ball and moves the ball to
              that position on the server. */
            RpcPickupBall(touchBall.gameObject);
            /* Runs the pick up script on all clients using the connection to the client that
            picked up the ball. */
            touchBall.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    [ClientRpc] // Specifies code that runs on all connected clients
    void RpcPickupBall(GameObject touchBall)
    {
        touchBall.transform.SetParent(HoldingPosition.transform, true);
        /* Sets the holding position as the parent of the ball and moves the ball to
        that position but only for the client who has a matching connection to
        the client who picked it up. */
        
    }
}
