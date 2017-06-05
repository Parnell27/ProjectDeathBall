using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : NetworkBehaviour
{
    public enum Team : byte { Team1 = 1, Team2 = 2 }; //2 teams are declared as enums

    [SyncVar(hook = "OnTeamSync")]
    private Team team = Team.Team1; //The enum is being assigned a default team.

    Color team1Colour = new Color(255, 0, 0, 1); //A new red colour for team 1
    Color team2Colour = new Color(0, 0, 255, 1); //A new blue colour from team 2

    //An attribute on the player setting their team and running the method to update their colour
    public Team PlayerTeam
    {
        get { return team; }

        set
        {
            team = value; //Sets a team the value of value
            UpdateTeamColour(); //Assigns that team a colour
        }
    }

    //When team value synched the colour value is updated
    void OnTeamSync(Team value)
    {
        PlayerTeam = value; //The new team is assigned value
    }

    //A method for updating the colour of a playeer based on their team
    private void UpdateTeamColour()
    {
        if (team == Team.Team1)
        {
            MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
            //Gets the mesh renderer component of the object this script is attached to

            gameObjectRenderer.material.color = team1Colour;
            //Applies the colour red if the player is on team 1
        }
        else
        {
            MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
            //Gets the mesh renderer component of the object this script is attached to

            gameObjectRenderer.material.color = team2Colour;
            //Applies the colour blue if the player is on team 2
        }
    }

    //Overrides the network manager's OnStartClient method to assign players a colour when they join
    public override void OnStartClient()
    {
        base.OnStartClient();
        UpdateTeamColour();
    }

    [Server] //Specifies that the following method runs on the server

    //Specifies the size of each team and increments the smallest team when a player joins
    public static Team GetSmallestTeam()
    {
        int team1Size = 0, team2Size = 0; //Sets the size of both teams to 0

        NetworkPlayerController[] players = FindObjectsOfType<NetworkPlayerController>();
        //Finds the player's NetworkPlayerController script

        //Cycles through all connected players and updates the team sizes based on the teams
        foreach (NetworkPlayerController player in players)
        {
            if (player.team == Team.Team1)
                team1Size++; //Increments the value of team1Size if the player's team is team 1

            else
                team2Size++; //Increments the value of team2Size if the player's team is team 2

        }

        //Returns the team that a new player should be added to
        if (team1Size <= team2Size)
            return Team.Team1; //Returns team 1 is team 1 is smaller than or equal to the size of team 2

        else
            return Team.Team2; //Retruns team 2 is it is smaller than team 1

    }

}