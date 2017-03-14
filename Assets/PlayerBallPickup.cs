using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallPickup : PlayerController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void BallPickup(Collision touchBall)
    {
        if (touchBall.gameObject.name == "BallPrefab")
        {
            Debug.Log("Ball touched");
        }
    }
}
