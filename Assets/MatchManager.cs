using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour {

   public static MatchManager singleton; //Declares a new singleton

    //Runs when the object attached to becomes active
    public void Awake()
	{
		singleton = this; //Indicates that this script is a singleton
	}

    //Runs when the object attached to is destroyed
    public void OnDestroy()
	{
		singleton = null; //Gives the singleton a value of null
	}

	//public int team1Score;
 //   public int team2Score;

	//void ResetScores()
	//{
	//	team1Score = team2Score = 0;
	//}

 //   // Use this for initialization
 //   void Start () {
 //       ResetScores();
 //   }

 //   public Text endText;

 //   public void EndMatch()
 //   {
 //       if (team1Score > team2Score)
 //       {
 //           endText.text = "Team 1 Wins!";
 //       }
 //       else if (team2Score > team1Score)
 //       {
 //           endText.text = "Team 2 Wins!";
 //       }
 //       else
 //       {
 //           endText.text = "Draw";
 //       }

 //       ShowPanels.singleton.ShowEndPanel();
 //   }
}
