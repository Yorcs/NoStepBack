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
            player.Jump();
        }
        if (controller.isLeftButton())
        {
            player.WalkHorizontal(-1);
        }
        if (controller.isDownButton()) {
            player.Crouch();
        }
        if(controller.releaseCrouch()) {
            player.ReleaseCrouch();
        }
        if (controller.isForwardButton()) {
            player.WalkHorizontal(1);
        }
        if (controller.isSpecialButton()) {
            player.Special();
        }
    }

    public void SetController(Controller controller) {
        this.controller = controller;
    }
}
