using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public static UIManager singleton;

    public Text deathTimerText;
    public Text matchTimer;

    public Text team1ScoreText;
    public Text team2ScoreText;

    // Use this for initialization
    void Start () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
