using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Elevator : MonoBehaviour {
    [SerializeField] private Tilemap door;

    private List<Collider2D> playersInElevator = new();

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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playersInElevator.Add(other);
            if(playersInElevator.Count >= GameFlowManager.instance.GetPlayerCount()) {
                //all players in Elevator
                CloseDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(playersInElevator.Contains(other))
                playersInElevator.Remove(other);
        }
    }
}
