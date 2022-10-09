using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasdKeyboard : MonoBehaviour, IController {

    private bool upAxisDownLast;
    private bool downAxisDownLast;

    public bool isUpButton() {
        if (Input.GetButtonDown("WasdUp")) {
            return true;
        }
        return false;
    }

    public bool isDownButton() {
        if (Input.GetButtonDown("WasdDown")) {
            return true;
        }
        return false;
    }

    public bool isForwardButton() {
        if (Input.GetAxisRaw("WasdHorizontal") == 1) {
            return true;
        }
        return false;
    }

    public bool isSpecialButton() {
        if (Input.GetButtonDown("WasdSpecial")) {
            return true;
        }
        return false;
    }
}
