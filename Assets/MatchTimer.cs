using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MatchTimer : Timer {

    

    // Use this for initialization
	protected void Start ()
    {
        endTime = (float) Network.time + 30;
        UnityEngine.Debug.Log("Match Timer started.");
	}

    void Update()
    {
        CheckTime();
    }

    void CheckTime()
    {
        double currentTime = endTime - Network.time;
        try
        {
            UIManager.singleton.matchTimer.text = currentTime.ToString("F0");
            //Attempts to run the code while detecting any errors
        }
        catch (NullReferenceException)
        {
            //Catches any null reference errors and makes them execeptions so the code can run ignoring them
        }

        if (NetworkServer.active && Network.time > endTime)
        {
            TimerExpire();
        }
    }

    public override void TimerExpire()
    {
        Application.Quit();
    }


}