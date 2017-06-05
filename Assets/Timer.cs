using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Timer : NetworkBehaviour {

    public float startTime; //An empty variable for the startTime of a timer
    public float endTime;   //An empty variable for the endTime of a timer

    public abstract void TimerExpire();
    //Declaration of an abstract method that will be overriden within each child class


}
