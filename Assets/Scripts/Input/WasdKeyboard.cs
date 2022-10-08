using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasdKeyboard : MonoBehaviour, IController {

    public bool isUpButton() {
        if (Input.GetKeyDown(KeyCode.W)) {
            return true;
        }
        return false;
    }

    public bool isDownButton() {
        if (Input.GetKeyDown(KeyCode.S)) {
            return true;
        }
        return false;
    }

    public bool isForwardButton() {
        if (Input.GetKey(KeyCode.D)) {
            return true;
        }
        return false;
    }

    public bool isSpecialButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            return true;
        }
        return false;
    }
}
