using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour {

    private CameraMovement cam;
    [SerializeField] private PVPManager pvp;

    //Todo: there's probably better options here
    [SerializeField] private List<string> levels;
    private int nextLevel = 0;

    private void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
        Assert.IsNotNull(cam);
        Assert.IsNotNull(pvp);
    }

    public void StartPVP() {
        cam.SetLocked(true);

        pvp.TriggerPVPCountdown();
    }

    public void EndPVP() {
        StartLevel();
    }

    public void StartLevel() {
        SceneManager.LoadSceneAsync(levels[nextLevel], LoadSceneMode.Additive);
        switch(nextLevel) {
        case 0:
            Elevator elevator = FindObjectOfType<Elevator>();
            Assert.IsNotNull(elevator);

            elevator.OpenDoor();
            UnlockCamera();
            break;

        case 1:

            break;
        default:
            break;
        }
        nextLevel++;
    }

    public void UnlockCamera() {
        cam.SetLocked(false);
    }

}
