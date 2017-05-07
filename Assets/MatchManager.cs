using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : MonoBehaviour {

	public static MatchManager singleton;

	public void Awake()
	{
		singleton = this;
	}

	public void OnDestroy()
	{
		singleton = null;
	}

	public int team1Score;
    public int team2Score;

	void ResetScores()
	{
		team1Score = team2Score = 0;
	}

    // Use this for initialization
    void Start () {
		ResetScores();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
