using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer : MonoBehaviour {

    public float startTime;

    public void StartTimer()
    {
        float currentTime = startTime;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);
        }
        //Decrements the timer time by 1 each second while there is time remaining

        TimerExpire();
    }

    public abstract void TimerExpire();
    //Declaration of an abstract method that will be overriden within each child class


}
