using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour {
    public static GameFlowManager instance;

    private CameraMovement cam;
    [SerializeField] private PVPManager pvp;
    [SerializeField] private BackgroundManager bgManager;

    Elevator elevator;
    [SerializeField] private Laser laser;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CutsceneUI cutscenePlayer;

    //Todo: there's probably better options here
    [SerializeField] private List<string> levels;
    [SerializeField] private List<CutsceneSO> cutscenes;
    [SerializeField] private ScoreUI scoreUIManager;
    private List<Score> scorePanels = new();
    private int OpenScorePanels = 0;


    private List<PlayerStatus> players = new();
    private int gameState = 0;
    private int level = 0;

    private int Level1BossPosition = 159;
    private int Level2ElevatorPosition = 244;
    private int Level2BossPosition = 760;
    private int Level3ElevatorPosition = 846;
    private int Level3BossPosition = 1275;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
        Assert.IsNotNull(laser);
        laser.LaserOff();
        Assert.IsNotNull(bgManager);
        Assert.IsNotNull(cam);
        Assert.IsNotNull(pvp);
        Assert.IsNotNull(audioManager);
        Assert.IsNotNull(cutscenePlayer);

        audioManager.Play("MenuMusic");
    }

    public void StartPVP() {
        laser.LaserOff();
        cam.SetLocked(true);

        pvp.TriggerPVPCountdown();
    }

    public void EndPVP() {   
        StartLevel();
    }

    public void StartLevel() {
        switch(gameState) {
        case 0:
            //Cutscene Pre-PVP 1
            cutscenePlayer.PlayCutscene(cutscenes[0]);
            break;
        
        case 1:
            //trigger PVP 1
            StartPVP();
            break;

        case 2:
            //Cutscene Post-PVP 1
            cutscenePlayer.PlayCutscene(cutscenes[1]);
            break;

        case 3:
            //Begin level 1
            audioManager.Stop("MenuMusic");
            audioManager.Play("Track1");
            
            SceneManager.LoadScene(levels[0], LoadSceneMode.Additive);
            elevator = FindObjectOfType<Elevator>();
            Assert.IsNotNull(elevator);

            elevator.OpenDoor();
            UnlockCamera();
            laser.LaserOn();
            bgManager.changeBackground(level - 1);
            cam.SetMaxPosition(Level1BossPosition);
            
            level = 1;
            break;

        case 4:
            //Cutscene PostBoss 1
            cutscenePlayer.PlayCutscene(cutscenes[2]);
            break;

        case 5:
            // Trigger PVP 2
            StartPVP();
            break;

        case 6:
            //release Players into elevator
            // elevator.OpenDoor();
            UnlockCamera();
            cam.SetMaxPosition(Level2ElevatorPosition);
            break;
        
        case 7:
            //Post level 1 UI
            elevator.CloseDoor();
            foreach(Score scorePanel in scorePanels) {
                scorePanel.gameObject.SetActive(true);
                scorePanel.DisplayScore();
                OpenScorePanels++;
            }
            break;

        case 8:
            //Cutscene ElevatorToLevel2
            cutscenePlayer.PlayCutscene(cutscenes[3]);
            break;

        case 9: 
            //Level 2
            audioManager.Stop("Track1");
            audioManager.Play("Track2");

            //SceneManager.UnloadSceneAsync(levels[0]);
            SceneManager.LoadScene(levels[1], LoadSceneMode.Additive);
            elevator.OpenDoor();
            UnlockCamera();
            cam.SetMaxPosition(Level2BossPosition);
            laser.LaserOn();
            bgManager.changeBackground(level - 1);

            level = 2;
            break;

        case 10:
            //cutscene Postboss 2
            cutscenePlayer.PlayCutscene(cutscenes[4]);
            break;

        case 11:
            //trigger PVP 3
            StartPVP();
            break;

        case 12:
        //     elevator.OpenDoor();
            UnlockCamera();
            cam.SetMaxPosition(Level3ElevatorPosition);
            break;

        case 13:
            //Show post Level 2 UI
            elevator.CloseDoor();
            foreach(Score scorePanel in scorePanels) {
                scorePanel.gameObject.SetActive(true);
                scorePanel.DisplayScore();
                OpenScorePanels++;
            }
            break;
            
        case 14:
            //Cutscene Elevator to level 3
            cutscenePlayer.PlayCutscene(cutscenes[5]);
            break;

        case 15:
            audioManager.Stop("Track2");
            audioManager.Play("Track3");

            SceneManager.UnloadSceneAsync(levels[0]);
            SceneManager.LoadScene(levels[2], LoadSceneMode.Additive);
            elevator.OpenDoor();
            UnlockCamera();
            cam.SetMaxPosition(Level3BossPosition);
            laser.LaserOn();
            bgManager.changeBackground(level - 1); //this is magic, but its Right

            level = 3;
            break;

        case 16:
            //cutscene postboss 3
            cutscenePlayer.PlayCutscene(cutscenes[6]);
            break;

        case 17:
            // trigger PVP final
            StartPVP();
            break;

        case 18:
            //show ending screen
            foreach(Score scorePanel in scorePanels) {
                scorePanel.gameObject.SetActive(true);
                scorePanel.DisplayScore();
                OpenScorePanels++;
            }
            break;

        default:
            SceneManager.LoadScene("Main Menu");
            break;
        }
        gameState++;
    }

    public int GetLevel() {
        return level;
    }

    public void UnlockCamera() {
        cam.SetLocked(false);
    }

    public int GetPlayerCount() {
        return players.Count;
    }

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus playerStatus = input.gameObject.GetComponent<PlayerStatus>();
        Assert.IsNotNull(playerStatus);
        players.Add(playerStatus);
    }

    public void AddScorePanel(Score panel) {
        scorePanels.Add(panel);
    }

    public void CloseScorePanel(Score panel) {
        panel.gameObject.SetActive(false);
        OpenScorePanels--;
        if(OpenScorePanels <= 0) {
            StartLevel();
        }
    }

}
