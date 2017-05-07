using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTimer : Timer {
    
    // Use this for initialization
	void Start () {
        startTime = 10;
        StartTimer();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override void TimerExpire()
    {
        Debug.Log("Timer Expired");

    }

}
