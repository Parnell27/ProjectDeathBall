using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathManager : NetworkBehaviour {
    
    public void PlayerDie()
    {
        GetComponent<PlayerBallPickup2>().DropBall();
        transform.position = DeathBallNetworkManager.GetStartPosition(GetComponent<NetworkPlayerController>().PlayerTeam).position;
        /* Teleports the player to a new start position of the team they are on. GetStartPosition will return a random spawn
        position from the list available to that team. The player's team is checked and used to specify which spawn positions
        they can be sent to. Theya re then moved to one at random */


    }

    [TargetRpc] //Tells each individual client to run some code (unlike client rpc which runs it for all clients together)
    public void TargetPlayerDie(NetworkConnection conn)
    {
        GetComponent<PlayerBallPickup2>().DropBall();
        transform.position = DeathBallNetworkManager.GetStartPosition(GetComponent<NetworkPlayerController>().PlayerTeam).position;
    }

}
