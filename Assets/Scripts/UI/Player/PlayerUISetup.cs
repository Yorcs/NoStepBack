using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUISetup : MonoBehaviour {
    [SerializeField] private List<PlayerHealthUI> healthUIs = new();

    [SerializeField] private List<PlayerWeaponUI> weaponUIs = new();

    [SerializeField] private List<PlayerMoneyUI> moneyUIs = new();

    [SerializeField] private List<CharacterSelectUI> charSelUIs = new();
    private int nextUI = 0;

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus status = input.gameObject.GetComponent<PlayerStatus>();
        PlayerActions actions = input.gameObject.GetComponent<PlayerActions>();
        PlayerUIController uiController = input.gameObject.GetComponent<PlayerUIController>();
        status.SetUI(healthUIs[nextUI], moneyUIs[nextUI]);
        actions.SetUI(weaponUIs[nextUI]);

        charSelUIs[nextUI].gameObject.SetActive(true);
        charSelUIs[nextUI].SetPlayerNumber(nextUI);
        uiController.SetUI(charSelUIs[nextUI]);
        uiController.SetActive(true);

        
        nextUI++;
    }

    public void ActivateGameUI(int playerIndex) {
        Debug.Log(playerIndex);
        charSelUIs[playerIndex].gameObject.SetActive(false);
        healthUIs[playerIndex].gameObject.SetActive(true);
        moneyUIs[playerIndex].gameObject.SetActive(true);
        weaponUIs[playerIndex].gameObject.SetActive(true);
    }
}
