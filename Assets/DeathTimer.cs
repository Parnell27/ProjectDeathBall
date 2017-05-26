using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTimer : Timer {

    void Start()
    {
        startTime = 5f; //Assigns a value to startTime;
        try
        {
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0");
            //Attempts to run the code while detecting any errors
        }
        catch (NullReferenceException)
        {
            //Catches any null reference errors and makes them execeptions so the code can run ignoring them
        }
    }

    public IEnumerator TimerCountdown()
    {
        Debug.Log("Death Timer started: " + Time.time);

        for (int i = 1; i <= startTime; i++)
        {
            yield return new WaitForSeconds(1);
            double currentTime = startTime - i;
            if (GetComponent<PlayerBallPickup2>().ballHeld == true)
            {
                UIManager.singleton.deathTimerText.text = currentTime.ToString("F0");
            }
        }
        //waits for 5 seconds, updating the timer UI element on every tick

        //Checks to see if the ball is held by the end of the timer to know if the player should be killed
        if (GetComponent<PlayerBallPickup2>().ballHeld == true)
        {
            print("Player has died: " + Time.time); //Logs a death message in the console and the time it happened
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0");
            TimerExpire(); //Calls the timer expiry method at the end of the timer duration
            yield break;
        }

        else
        {
            UIManager.singleton.deathTimerText.text = startTime.ToString("F0");
            yield break; //Exits the coroutine if the ball isnt held upon expiry
        }

    }

    public override void TimerExpire()
    {
        GetComponent<DeathManager>().PlayerDie();
        //Retrieves the DeathManager script and calls the PlayerDie function upon timer expiry
    }

}
