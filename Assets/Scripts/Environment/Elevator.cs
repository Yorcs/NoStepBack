using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Elevator : MonoBehaviour {
    [SerializeField] private Tilemap door;

    public void OpenDoor() {
        door.gameObject.SetActive(false);
    }
}
