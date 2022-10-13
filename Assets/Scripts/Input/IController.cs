using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController {
    bool isForwardButton();
    bool isUpButton();
    bool isDownButton();
    bool isSpecialButton();
}
