using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBallStartPosition : MonoBehaviour {

    public NetworkPlayerController.Team team; //Creates a local instance of the Team enum

    //Called when the object attached to becomes active
    void Awake()
    {
        DeathBallNetworkManager.RegisterStartPosition(transform, team);
        //Calls the RegisterStartPosition method from the DeathBallNetworkManager to register start positions when the game starts
    }

    //Called when the object attached to is destroyed
    void OnDestroy()
    {
        DeathBallNetworkManager.UnregisterStartPosition(transform, team);
        //Calls the UnregisterStartPosition method from the DeathBallNetworkManager to unregister the start positions when this script is destroyed
    }
}
