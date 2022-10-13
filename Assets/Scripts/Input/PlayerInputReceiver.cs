using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class PlayerInputReceiver : MonoBehaviour {

    [SerializeField] private Controller controller;
    public Player player;

    private void Start() {
        Assert.IsNotNull(controller);
    }

    void Update() {
        if (controller.isUpButton()) {
            Debug.Log("up");
            player.Jump();
        }
        if (controller.isDownButton()) {
            Debug.Log("crouch");
            player.Crouch();
        }
        if (controller.isForwardButton()) {
            Debug.Log("walk");
            player.Walk();
        }
        if (controller.isSpecialButton()) {
            Debug.Log("special");
            player.Special();
        }
    }

    public void SetController(Controller controller) {
        this.controller = controller;
    }
}
