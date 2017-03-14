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

    void SpawnBall()
    {
        var ball = (GameObject)Instantiate(ballPrefab, transform.position, transform.rotation);
        /*Creates an instance of the ball using the ball prefab and specifying a position
        and rotation to spawn the ball at thata is the same position as the ball spawner object */

        NetworkServer.Spawn(ball);
        //Spawns the ball on the server so it spawns for all players
    }
}
