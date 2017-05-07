using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour {

    public int team1Score;
    public int team2Score;

    // Use this for initialization
    void Start () {
        team1Score = 0;
        team2Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
