using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Elevator : MonoBehaviour {
    [SerializeField] private Tilemap door;

    

    private bool closed;
    private float closedTimer = 5f;

    public void Update() {
        if(closed) {
            closedTimer -= Time.deltaTime;
            if(closedTimer <= 0) {
                closed = false;
                GameFlowManager.instance.StartLevel();
            }
        }
    }

    public void OpenDoor() {
        closed = false;
        door.gameObject.SetActive(false);
    }

    public void CloseDoor() {
        closed = true;
        closedTimer = 5f;
        door.gameObject.SetActive(true);
    }

    
}
