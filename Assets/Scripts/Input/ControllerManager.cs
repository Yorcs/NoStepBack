using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {
    List<PlayerInputReceiver> inputReceivers = new List<PlayerInputReceiver>();
    List<Controller> controllers = new List<Controller>();

    void Awake() {  
        inputReceivers.AddRange(GetComponentsInChildren<PlayerInputReceiver>());
        controllers.AddRange(GetComponentsInChildren<Controller>());

        inputReceivers[0].SetController(controllers[0]);
        inputReceivers[1].SetController(controllers[1]);
    }

}
