using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : Timer {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TimerExpire()
    {
        Debug.Log("Timer Expired");

        throw new NotImplementedException();
    }

}
