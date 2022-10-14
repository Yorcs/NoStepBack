using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField] string upButton;
    [SerializeField] string downButton;
    [SerializeField] string horizontalAxis;
    [SerializeField] string specialButton;

    public bool isUpButton() {
        if (Input.GetButtonDown(upButton)) {
            return true;
        }
        return false;
    }

    public bool isDownButton() {
        if (Input.GetAxisRaw(downButton) == -1) {
            return true;
        }
        return false;
    }

    public bool isForwardButton() {
        if (Input.GetAxisRaw(horizontalAxis) == 1) {
            return true;
        }
        return false;
    }

    public bool isLeftButton()
    {
        if (Input.GetAxisRaw(horizontalAxis) == -1)
        {
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
