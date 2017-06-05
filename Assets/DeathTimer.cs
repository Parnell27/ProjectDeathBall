using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTimer : Timer {

    void Start()
    {
        startTime = 5f; //Assigns startTime the duration of the death timer;
        try
        {
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0");
            //Changes the death timer text on the HUD to the value of startTime and converted to a format without decimal point numbers
            //Attempts to run the code while detecting any errors
        }
        catch (NullReferenceException)
        {
            //Catches any null reference errors and makes them execeptions so the code can run ignoring them
        }
    }

    //A coroutine controlling the death timer ticking and what to do upon expiry
    public IEnumerator TimerCountdown()
    {
        Debug.Log("Death Timer started: " + Time.time); //Indicates that the timer has started

        for (int i = 1; i <= startTime; i++)
        {
            yield return new WaitForSeconds(1);
            double currentTime = startTime - i;
            if (GetComponent<PlayerBallPickup2>().ballHeld == true)
            {
                UIManager.singleton.deathTimerText.text = currentTime.ToString("F0");
            }
        }
        /*Waits for 5 seconds, reducing the value of the currentTime by 1 each time and updating the timer UI element on every
        tick while the ball is held */

        //Checks to see if the ball is held by the end of the timer to know if the player should be killed
        if (GetComponent<PlayerBallPickup2>().ballHeld == true)
        {
            print("Player has died: " + Time.time); //Logs a death message in the console and the time it happened
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0"); //Resets the death timer UI element to its start time
            TimerExpire(); //Calls the timer expiry method at the end of the timer duration
            yield break; //Breaks the coroutine
        }

        else
        {
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0"); //Resets the death timer UI element to its start time
            yield break; //Exits the coroutine if the ball isnt held upon expiry
        }

    }

    //An override of the TimerExpire method on the parent Timer script, to specify what to do when the timer duration ends
    public override void TimerExpire()
    {
        GetComponent<DeathManager>().PlayerDie();
        //Retrieves the DeathManager script and calls the PlayerDie function upon timer expiry
    }

}
