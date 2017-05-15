using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBallNetworkManager : NetworkManager
{

    //Menu Background

    public GameObject menuBackground;
    public GameObject MatchManager;

    public override void OnStartHost()
    {
        base.OnStartHost(); //Preserves the existing code within the function
        menuBackground.SetActive(false); //Hides the menu background for the host when the game is hosted
        MatchManager.SetActive(true); //Activates the match manager when the game is hosted
    }
    //Overrides the functionality of the network manager OnStartHost function to disable the menu background

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn); //Preserves the existing code within the function
        menuBackground.SetActive(false); //Hides the menu background for a client when they connect
    }

    //Start positions and teams

    public static List<Transform> team1Spawns = new List<Transform>(); //Creates a new list for the spawn points of team 1
    public static List<Transform> team2Spawns = new List<Transform>(); //Creates a new list for the spawn points of team 2

    public static void RegisterStartPosition(Transform pos, NetworkPlayerController.Team team)
    {
        if (team == NetworkPlayerController.Team.Team1)
        {
            team1Spawns.Add(pos);
            //Adds the transform pos, taken from the function parameters to the list of team 1's spawn points
        }
        else
        {
            team2Spawns.Add(pos);
            //Adds the transform pos, taken from the function parameters to the list of team 2's spawn points
        }

    }

    public static void UnregisterStartPosition(Transform pos, NetworkPlayerController.Team team)
    {
        if (team == NetworkPlayerController.Team.Team1)
        {
            team1Spawns.Remove(pos);
            //Removes the transform pos, taken from the function parameters to the list of team 2's spawn points
        }
        else
        {
            team2Spawns.Remove(pos);
            //Removes the transform pos, taken from the function parameters to the list of team 2's spawn points
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }
    //Runs the internal adding player script

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }
    /*The network manager will run both scripts by default, therefore there are 2 identical methods, except for
    the extra parameter in one*/

    void OnServerAddPlayerInternal(NetworkConnection conn, short playerControllerId)
    {
        if (playerPrefab == null)
        {
            if (LogFilter.logError) { Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object."); }
            return;
            //Logs an error to the console when no player prefab exists.
        }

        if (playerPrefab.GetComponent<NetworkIdentity>() == null)
        {
            if (LogFilter.logError) { Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab."); }
            return;
            //Logs an error to the console when a player prefab doesn't contain a network identity.
        }

        if (playerControllerId < conn.playerControllers.Count && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null)
        {
            if (LogFilter.logError) { Debug.LogError("There is already a player at that playerControllerId for this connections."); }
            return;
            /* Logs an error to the console when one or more of multiple criteria are untrue. If the ID of the added player
            exceeds the number of player controllers; if an invalid player controller is used by the connected player; or 
            if the object the player controller is attached to doesn't exist; an error message will be logged and displayed. */
        }

        GameObject player;

		NetworkPlayerController.Team newPlayerTeam = NetworkPlayerController.GetSmallestTeam(); 
        //Creates a new team instance, using the GetSmallestTeam method to specify which team it is

        Transform startPos = GetStartPosition(newPlayerTeam);
        //Declares a new transform to store the start position of the newly created team

        if (startPos != null)
        {
            player = Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

		NetworkPlayerController playerController = player.GetComponent<NetworkPlayerController>();
		playerController.PlayerTeam = newPlayerTeam;

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public static Transform GetStartPosition(NetworkPlayerController.Team team)
    {
        if (team == NetworkPlayerController.Team.Team1)
        {
            int element = Random.Range(0, team1Spawns.Count - 1);
            return team1Spawns[element];
        }
        else
        {
            int element = Random.Range(0, team2Spawns.Count - 1);
            return team2Spawns[element];
        }
    }
}