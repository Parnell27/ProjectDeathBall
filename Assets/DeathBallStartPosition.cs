using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBallStartPosition : MonoBehaviour {

    public NetworkPlayerController.Team team;

    void Awake()
    {
        DeathBallNetworkManager.RegisterStartPosition(transform, team);
    }

    void OnDestroy()
    {
        DeathBallNetworkManager.UnregisterStartPosition(transform, team);
    }
}
