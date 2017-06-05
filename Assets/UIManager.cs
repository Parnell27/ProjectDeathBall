using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public static UIManager singleton; //Declares a new singleton under the same name as the class

    public Text deathTimerText; //Specifies which text element is for the death timer's value
    public Text matchTimer;     //Specifies which text element is for the match timer's value

    public Text team1ScoreText; //Specifies which text element is for team 1's score value
    public Text team2ScoreText; //Specifies which text element is for team 2's score value

    public Text endGameText;    //Specifies which text element is for the end game text

    // Use this for initialization
    void Start () {
        singleton = this; //Declares this script as the singleton
	}
}
