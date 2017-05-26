using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoringManager : NetworkBehaviour {

    public int TeamNumber;
    public GameObject ballSpawnPoint;

    void Start()
    {
        try
        {
            UIManager.singleton.team1ScoreText.text = MatchManager.singleton.team1Score.ToString();
            UIManager.singleton.team2ScoreText.text = MatchManager.singleton.team2Score.ToString();
            //Attempts to run the code while detecting any errors
        }
        catch (NullReferenceException)
        {
            //Catches any null reference errors and makes them execeptions so the code can run ignoring them
        }

    }

    void OnTriggerEnter(Collider ball)
    {
        if (!NetworkServer.active)
        {
            return;
        }

        if (ball.gameObject == BallSpawn.ballObject)
        {
            ServerUpdateScore();
            ball.transform.position = ballSpawnPoint.transform.position;
            foreach (NetworkConnection conn in NetworkServer.connections)
            {
                conn.playerControllers[0].gameObject.GetComponent<DeathManager>().TargetPlayerDie(conn);
                //Finds all connected players and run the target rpc kill script on them to respawn them after a point is scored
            }      
        }
    }

    [Server]
    void ServerUpdateScore()
    {
        if (TeamNumber == 1)
        {
            MatchManager.singleton.team2Score += 1;
            Debug.Log("Team 2 has scored.");
            //UIManager.singleton.team2ScoreText.GetComponent<Text>();
            UIManager.singleton.team2ScoreText.text = MatchManager.singleton.team2Score.ToString();
            
        }
        else if (TeamNumber == 2)
        {
			MatchManager.singleton.team1Score += 1;
            Debug.Log("Team 1 has scored.");
            //UIManager.singleton.team1ScoreText.GetComponent<Text>();
            UIManager.singleton.team1ScoreText.text = MatchManager.singleton.team1Score.ToString();
        }

        Debug.Log("New score " + MatchManager.singleton.team1Score + " : " + MatchManager.singleton.team2Score);
        //RpcUpdateScore();

    }


    [ClientRpc]

    void RpcUpdateScore()
    {
        //if (TeamNumber == 1)
        //{
        //    MatchManager.singleton.team1Score += 1;
        //    Debug.Log("Team 1 has scored.");
        //}
        //else if (TeamNumber == 2)
        //{
        //    MatchManager.singleton.team2Score += 1;
        //    Debug.Log("Team 2 has scored.");
        //}
    }
}
