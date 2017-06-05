using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UpdateScoreUI : NetworkBehaviour
{

    // Update is called once per frame
    void Update()
    {
        UpdateScores(); //Calls the UpdateScores method each frame
    }

    //Updates the score UI elements
    void UpdateScores()
    {
        UIManager.singleton.team1ScoreText.text = ScoringManager.singleton.scoreTeam1Text;
        //Updates team 1's score UI element to display the value of their score converted to a string
        UIManager.singleton.team2ScoreText.text = ScoringManager.singleton.scoreTeam2Text;
        //Updates team 2's score UI element to display the value of their score converted to a string
        //RpcUpdateScores(); //Calls RpcUpdateScores

    }

    //[ClientRpc]
    ////Updates the score UI elements on all clients
    //void RpcUpdateScores()
    //{
    //    UIManager.singleton.team1ScoreText.text = ScoringManager.singleton.scoreTeam1Text;
    //    //Updates team 1's score UI element to display the value of theor score converted to a string
    //    UIManager.singleton.team2ScoreText.text = ScoringManager.singleton.scoreTeam2Text;
    //    //Updates team 2's score UI element to display the value of theor score converted to a string
    //}

    //Handles what code to run when the game ends
    public void EndMatch()
    {
        //Runs the following code when team 1 has a higher score than team 2 when the match ends
        if (ScoringManager.singleton.scoreTeam1 > ScoringManager.singleton.scoreTeam2)
        {
            UIManager.singleton.endGameText.text = "Team 1 Wins!"; //Displays a team 1 victory message on the end game UI element
        }
        
        //Runs the following code when team 2 has a higher score than team 1 when the match ends
        else if (ScoringManager.singleton.scoreTeam2 > ScoringManager.singleton.scoreTeam1)
        {
            UIManager.singleton.endGameText.text = "Team 2 Wins!"; //Displays a team 2 victory message on the end game UI element
        }

        //Runs the following code when the scores of each team are equal
        else
        {
            UIManager.singleton.endGameText.text = "Draw"; //Diplays a draw message on the end game UI element
        }

        ShowPanels.singleton.ShowEndPanel(); //Calls the ShowEndPanel method
    }
}