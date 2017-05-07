using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoringManager : NetworkBehaviour {

    MatchManager matchManager = new MatchManager();
    public int TeamNumber;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider ball)
    {
        if (ball.gameObject == BallSpawn.ballObject)
        {
            ServerUpdateScore();
        }

    }

    [Server]

    void ServerUpdateScore()
    {
        if (TeamNumber == 1)
        {
            matchManager.team1Score += 1;
            Debug.Log("Team 1 has scored.");
        }
        else if (TeamNumber == 2)
        {
            matchManager.team2Score += 1;
            Debug.Log("Team 2 has scored.");
        }

        Debug.Log("New score " + matchManager.team1Score + " : " + matchManager.team2Score);
        RpcUpdateScore();

    }


    [ClientRpc]

    void RpcUpdateScore()
    {
        //if (TeamNumber == 1)
        //{
        //    matchManager.team1Score += 1;
        //    Debug.Log("Team 1 has scored.");
        //}
        //else if (TeamNumber == 2)
        //{
        //    matchManager.team2Score += 1;
        //    Debug.Log("Team 2 has scored.");
        //}

        //Debug.Log("New score " + matchManager.team1Score + " : " + matchManager.team2Score);
    }


}
