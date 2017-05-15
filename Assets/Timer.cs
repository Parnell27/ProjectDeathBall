using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer : MonoBehaviour {

    public float startTime;
    public float endTime;

    public abstract void TimerExpire();
    //Declaration of an abstract method that will be overriden within each child class


}
