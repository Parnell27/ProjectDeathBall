using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreDetection : NetworkBehaviour {

    public GameObject ballSpawnPoint;

    public int TeamNumber; //An empty variabel to be assigned the team of hoop this script is attached to

    //Runs code when an object is detected in the trigger area
    void OnTriggerEnter(Collider ball)
    {
        if (!NetworkServer.active)
        {
            return; //If the server isn't active, returns nothing
        }

        //Runs the following code when the detected object is the ball and the server is active
        if (ball.gameObject == BallSpawn.ballObject)
        {
            ServerUpdateScore(); //Calls the ServerUpdateScore method
            ball.transform.position = ballSpawnPoint.transform.position; //Moves the ball back to the position of its spawn point
            foreach (NetworkConnection conn in NetworkServer.connections)
            {
                conn.playerControllers[0].gameObject.GetComponent<DeathManager>().TargetPlayerDie(conn);
                //Finds all connected players and run the target rpc kill script on them to respawn them after a point is scored
            }
        }
    }

    [Server]
    //Handles how the score is updated on the server
    void ServerUpdateScore()
    {
        //Runs the following code when the hoop scored in belongs to team 2
        if (TeamNumber == 2)
        {
            ScoringManager.singleton.scoreTeam1 += 1; //Adds 1 to team 1's score total
            ScoringManager.singleton.scoreTeam1Text = ScoringManager.singleton.scoreTeam1.ToString(); //Converts team 1's score to a string
            Debug.Log("Team 1 has scored."); //Logs the goal to the console
        }

        else if (TeamNumber == 1)
        {
            ScoringManager.singleton.scoreTeam2 += 1; //Adds 1 to team 2's score total
            ScoringManager.singleton.scoreTeam2Text = ScoringManager.singleton.scoreTeam2.ToString(); //Converts team 2's score to a string
            Debug.Log("Team 2 has scored."); //Logs the goal to the console
        }

        Debug.Log("New score " + ScoringManager.singleton.scoreTeam1Text + " : " + ScoringManager.singleton.scoreTeam2Text);
        //Logs the new scores of each team match to the console
    }
}
