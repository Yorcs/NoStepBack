using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField] string upButton;
    [SerializeField] string downButton;
    [SerializeField] string forwardButton;
    [SerializeField] string specialButton;

    public bool isUpButton() {
        if (Input.GetButtonDown(upButton)) {
            return true;
        }
        return false;
    }

    public bool isDownButton() {
        if (Input.GetButtonDown(downButton)) {
            return true;
        }
        return false;
    }

    public bool isForwardButton() {
        if (Input.GetAxisRaw(forwardButton) == 1) {
            return true;
        }
        return false;
    }

    public bool isSpecialButton() {
        if (Input.GetButtonDown(specialButton)) {
            return true;
        }
        return false;
    }
}
