using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PVPManager : MonoBehaviour {
    [SerializeField] private List<PlayerStatus> players = new();
    private int[] teamLayers = new int[4];
    private List<List<PlayerStatus>> teams = new();
    
    // private int teamOneLayer;
    // private int teamTwoLayer;
    // private int teamThreeLayer;
    // private int teamFourLayer;
    private int defaultPlayerLayer;

    private bool pvpActive;

    public int goldReward = 200;
    

    private void Start() {
        teamLayers[0] = LayerMask.NameToLayer("Team 1");
        teamLayers[1] = LayerMask.NameToLayer("Team 2");
        teamLayers[2] = LayerMask.NameToLayer("Team 3");
        teamLayers[3] = LayerMask.NameToLayer("Team 4");
        defaultPlayerLayer = LayerMask.NameToLayer("Players");
        for(int i = 0; i < 4; i++) {
            teams.Add(new List<PlayerStatus>());
        }
    }

    public void StartPVP () {
        //Tell Players to go to PVP state
        foreach(PlayerStatus player in players) {
            player.SetPVP(true);
        }

        FreeForAll();

        pvpActive = true;
    }

    private void FreeForAll() {
        for (int i = 0; i < players.Count; i++) {
            teams[i].Add(players[i]);
            players[i].gameObject.layer = teamLayers[i];
        } 
    }

    // Update is called once per frame
    void Update() {
        //Testing functionality - Remove later
        if(Input.GetKeyDown("return")) {
            if(!pvpActive) {
                Debug.Log("Force Start PVP");
                StartPVP();            
            } else {
                Debug.Log("Force End PVP");
                EndPVP();
            }
        }

        if(!pvpActive) return;

        List<PlayerStatus> winningTeam = teams[0];
        int teamsRemaining = 0;
        foreach(List<PlayerStatus> team in teams) {
            if(IsTeamAlive(team)) {
                teamsRemaining++;
                winningTeam = team;
            }
        }

        if(teamsRemaining == 0) {
            //TODO: discuss something for here
            EndPVP();
        }

        if(teamsRemaining == 1) {
            foreach(PlayerStatus player in winningTeam) {
                player.GainMoney(goldReward);
            }
            EndPVP();
        }
    }

    private bool IsTeamAlive(List<PlayerStatus> players) {
        foreach(PlayerStatus player in players) {
            if(!player.IsDead()) {
                return true;
            }
        }
        return false;
    }

    private void EndPVP() {
        foreach(PlayerStatus player in players) {
            player.gameObject.layer = defaultPlayerLayer;
            player.SetPVP(false);
            player.Revive();
            player.FillHealth();
        }
        pvpActive = false;
    }

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus player = input.gameObject.GetComponent<PlayerStatus>();
        Assert.IsNotNull(player);
        players.Add(player);
    }
}
