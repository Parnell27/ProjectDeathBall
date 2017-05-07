using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBallNetworkManager : NetworkManager
{

    public GameObject menuBackground;

    public static List<Transform> team1Spawns = new List<Transform>();
    public static List<Transform> team2Spawns = new List<Transform>();

    public override void OnStartHost()
    {
        base.OnStartHost();
        menuBackground.SetActive(false);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        menuBackground.SetActive(false);
    }

    public static void RegisterStartPosition(Transform pos, NetworkPlayerController.Team team)
    {
        if (team == NetworkPlayerController.Team.Team1)
        {
            team1Spawns.Add(pos);
        }
        else
        {
            team2Spawns.Add(pos);
        }

    }

    public static void UnregisterStartPosition(Transform pos, NetworkPlayerController.Team team)
    {
        if (team == NetworkPlayerController.Team.Team1)
        {
            team1Spawns.Remove(pos);
        }
        else
        {
            team2Spawns.Remove(pos);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }

    void OnServerAddPlayerInternal(NetworkConnection conn, short playerControllerId)
    {
        if (playerPrefab == null)
        {
            if (LogFilter.logError) { Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object."); }
            return;
        }

        if (playerPrefab.GetComponent<NetworkIdentity>() == null)
        {
            if (LogFilter.logError) { Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab."); }
            return;
        }

        if (playerControllerId < conn.playerControllers.Count && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null)
        {
            if (LogFilter.logError) { Debug.LogError("There is already a player at that playerControllerId for this connections."); }
            return;
        }

        GameObject player;

		NetworkPlayerController.Team newPlayerTeam = NetworkPlayerController.GetSmallestTeam();
        Transform startPos = GetStartPosition(newPlayerTeam);
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

    public Transform GetStartPosition(NetworkPlayerController.Team team)
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