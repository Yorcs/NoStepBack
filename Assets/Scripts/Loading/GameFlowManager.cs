using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameFlowManager : MonoBehaviour {
    
    public void StartLevel() {
        Elevator elevator = FindObjectOfType<Elevator>();
        Assert.IsNotNull(elevator);

        elevator.OpenDoor();
    }

}
