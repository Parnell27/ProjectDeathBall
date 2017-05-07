using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : NetworkBehaviour
{


    public enum Team : byte { Team1 = 1, Team2 = 2 }; //2 teams are declared as enums

    [SyncVar(hook = "OnTeamSync")]
    private Team team = Team.Team1; //The enum is being assigned a defaut team.

    Color team1Colour = new Color(255, 0, 0, 1); //A new red colour for team 1
    Color team2Colour = new Color(0, 0, 255, 1); //A new blue colour from team 2

    //An attribute on the player setting their team and running the method to update their colour
    public Team PlayerTeam
    {
        set
        {
            team = value;
            UpdateTeamColour();
        }
    }

    //When team value synched the colour value is updated
    void OnTeamSync(Team value)
    {
        PlayerTeam = value;
    }

    //A method for updating the colour of a playeer based on their team
    private void UpdateTeamColour()
    {
        if (team == Team.Team1)
        {
            MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
            gameObjectRenderer.material.color = team1Colour;
            //Applies the colour red if the player is on team 1
        }
        else
        {
            MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
            gameObjectRenderer.material.color = team2Colour;
            //Applies the colour blue if the player is on team 2
        }
    }

    //Overrides the method that runs when the server starts to call the method that obtains the smallest team
    public override void OnStartServer()
    {
        base.OnStartServer();
        team = GetSmallestTeam();
    }

    [Server]
    //Specifies the size of each team andincrements the smallest team when a player joins
    public static Team GetSmallestTeam()
    {
        int team1Size = 0, team2Size = 0;

        NetworkPlayerController[] players = FindObjectsOfType<NetworkPlayerController>();
        //Finds the player's NetworkPlayerController script

        //Cycles through all connected players and updates the team sizes based on the teams
        foreach (NetworkPlayerController player in players)
        {
            if (player.team == Team.Team1)
                team1Size++;

            else
                team2Size++;

        }

        //Returns the team that a new player should be added to
        if (team1Size <= team2Size)
            return Team.Team1;

        else
            return Team.Team2;



    }

}