using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MatchTimer : Timer {

    public double currentTime; //A public variable to store the current time of the timer
    public double currentSec;  //A public variable to store the current time of the timer in seconds
    public double currentMin;  //A public variable to store the current time of the timer in minutes

    // Use this for initialization
    protected void Start ()
    {
        if (this.GetComponent<NetworkBehaviour>().isLocalPlayer)
            endTime = (float) Network.time + 30; //Stores the duration of the match timer within endTime by adding it to the current network time
        UnityEngine.Debug.Log("Match Timer started."); //Indicates that the match timer has started
	}

    void Update()
    {
        if(this.GetComponent<NetworkBehaviour>().isLocalPlayer)
            CheckTime(); //Calls the CheckTime method each frame
    }

    //Checks the current timer time
    void CheckTime()
    {
        currentTime = endTime - Network.time; //Calculates the current time by finding the difference between the current time and the end time

        UpdateTimerUI(); //Calls the method to update the match timer UI

        if (Network.time > endTime)
        {
            TimerExpire(); //When the surrent time has exceeded the end time, calls the TimerExire method
        }

        if (Network.time > endTime + 10)
        {
            Network.Disconnect(); //10 seconds after the end of the match, disconnects all clients
            Application.Quit(); //Closes the application
        }
    }

    //Updates the match timer UI element
    void UpdateTimerUI()
    {
        currentSec = currentTime % 60;
        //Calculates the current seconds value of the timer by finding the remainder of the current time divided by 60 seconds
        currentMin = currentTime / 60;
        //Caluclates the current minutes value of the timer by dividing the current time by 60 seconds

        UIManager.singleton.matchTimer.text = currentMin.ToString("F0") + ":" + currentSec.ToString("F0");
        //Updates the match timer UI element to display the current minutes and seconds after converting them to strings without decimal points

        Debug.Log("Server update UI is running"); //Logs that the UI update is occurring

    }

    //Overrides the TimerExpire method from its parent to run when the timer ends
    public override void TimerExpire()
    {
        GetComponent<UpdateScoreUI>().EndMatch(); //Gets the UpdateScoreUI script and runs the EndMatch method
    }


}