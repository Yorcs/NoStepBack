using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUISetup : MonoBehaviour {
    [SerializeField] private List<PlayerHealthUI> healthUIs = new();
    private int nextUI = 0;

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus status = input.gameObject.GetComponent<PlayerStatus>();
        healthUIs[nextUI].gameObject.SetActive(true);
        status.SetUI(healthUIs[nextUI]);
        nextUI++;
    }
}
