using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCollider : MonoBehaviour {
    private List<Collider2D> playersInElevator = new();
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playersInElevator.Add(other);
            Debug.Log("Player has boarded the Elevator");
            if(playersInElevator.Count >= GameFlowManager.instance.GetPlayerCount()) {
                //all players in Elevator
                GameFlowManager.instance.StartLevel();
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
