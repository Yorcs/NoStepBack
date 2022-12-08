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
    [SerializeField] private AudioManager audio;

    //Todo: there's probably better options here
    [SerializeField] private List<string> levels;

    private List<PlayerStatus> players = new();
    private int gameState = 0;

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
        laser.LaserOff();
        Assert.IsNotNull(bgManager);
        Assert.IsNotNull(cam);
        Assert.IsNotNull(pvp);
        Assert.IsNotNull(laser);
        Assert.IsNotNull(audio);

        audio.Play("MenuMusic");
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
            audio.Stop("MenuMusic");
            audio.Play("Track1");
            
            SceneManager.LoadScene(levels[0], LoadSceneMode.Additive);
            elevator = FindObjectOfType<Elevator>();
            Assert.IsNotNull(elevator);

            elevator.OpenDoor();
                UnlockCamera();
                laser.LaserOn();
                bgManager.changeBackground(gameState);
                cam.SetMaxPosition(Level1BossPosition);
            break;

        // case 1:
        //     elevator.OpenDoor();
        //     UnlockCamera();
        //     cam.SetMaxPosition(Level2ElevatorPosition);
        //     break;

        case 1:
            audio.Stop("Track1");
            audio.Play("Track2");

            //SceneManager.UnloadSceneAsync(levels[0]);
            SceneManager.LoadScene(levels[1], LoadSceneMode.Additive);
            elevator.OpenDoor();
            UnlockCamera();
            laser.LaserOn();
                bgManager.changeBackground(gameState);
                cam.SetMaxPosition(Level2BossPosition);
            break;

        // case 3:
        //     elevator.OpenDoor();
        //     UnlockCamera();
        //     cam.SetMaxPosition(Level3ElevatorPosition);
        //     break;

        case 2:
            audio.Stop("Track2");
            audio.Play("Track3");

            SceneManager.UnloadSceneAsync(levels[0]);
            SceneManager.LoadScene(levels[2], LoadSceneMode.Additive);
                elevator.OpenDoor();
            UnlockCamera();
            laser.LaserOn();
                bgManager.changeBackground(gameState);
                cam.SetMaxPosition(Level3BossPosition);
            break;
        
        //Case 5 ends the game
        //show ending screen

        default:
            break;
        }
        gameState++;
    }

    public void UnlockCamera() {
        cam.SetLocked(false);
    }

    public int GetPlayerCount() {
        return players.Count;
    }

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus player = input.gameObject.GetComponent<PlayerStatus>();
        Assert.IsNotNull(player);
        players.Add(player);
    }

}
