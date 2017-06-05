using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoringManager : NetworkBehaviour {

    public static ScoringManager singleton; //Declares a new singleton under the same name as this script

    [SyncVar]
    public int scoreTeam1 = 0; //An integer value for team 1's score total
    [SyncVar]
    public int scoreTeam2 = 0; //An integer value for team 2's score total

    [SyncVar]
    public string scoreTeam1Text; //An string value for team 1's score total
    [SyncVar]
    public string scoreTeam2Text; //An string value for team 2's score total

    void Start()
    {
        singleton = this; //Delcares that this script is the singleton
        ResetScores(); //Calls ResetScores when the match starts
    }

    //Resets each team's score to zero
    void ResetScores()
    {
        scoreTeam1 = scoreTeam2 = 0; //Sets the value of both score total to 0
        scoreTeam1Text = scoreTeam1.ToString(); //Sets the vakue if team 1's score text to their score total
        scoreTeam2Text = scoreTeam2.ToString(); //Sets the vakue if team 2's score text to their score total
    }


    //    [ClientRpc]

    //    void RpcUpdateScore()
    //    {
    //        if (TeamNumber == 1)
    //        {
    //            UIManager.singleton.team1ScoreText.text = MatchManager.singleton.team1Score.ToString();
    //        }
    //        else if (TeamNumber == 2)
    //        {
    //            UIManager.singleton.team2ScoreText.text = MatchManager.singleton.team2Score.ToString();
    //        }
    //    }

}
