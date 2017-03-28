using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallSpawn : NetworkBehaviour {

	// Use this for initialization
	void Start () {

        SpawnBall();
        //Spawns the ball upon hosting of the game

    }
	
	// Update is called once per frame
	void Update () {

    }


    public GameObject ballPrefab;
    //Creates an instance of a game object called ballPrefab

    public static GameObject ballObject;
    //Creates an instance of a game object called ball

    void SpawnBall()
    {
        ballObject = (GameObject)Instantiate(ballPrefab, transform.position, transform.rotation);
        /*Creates an instance of the ball using the ball prefab and specifying a position
        and rotation to spawn the ball at that is the same position as the ball spawner object.
        This information is all stored in the ball game object.*/

        NetworkServer.Spawn(ballObject);
        /*Spawns the ball game object on the server so it spawns the prefab for all players
        at the postion of the spawner. */
    }
}
