using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterSelectUI : MonoBehaviour {
    private PlayerUIController playerUIController;
    private PlayerUISetup uiManager;
    private List<ISelectable> options = new();

    private int currentSelection = 0;
    private int playerIndex = 0;

    public void Start() {
        uiManager = GetComponentInParent<PlayerUISetup>();
        Assert.IsNotNull(uiManager);
    }

    public void SetPlayerNumber(int playerIndex) {
        this.playerIndex = playerIndex;
    }

    public void Setup(PlayerUIController playerUIController) {
        this.playerUIController = playerUIController;
        options.AddRange(GetComponentsInChildren<ISelectable>());

        foreach (ISelectable option in options) {
            option.Setup(playerUIController);
        }
    }

    public void MoveUp() {
        if(currentSelection == 0) currentSelection = options.Count;
        currentSelection = (currentSelection - 1) % options.Count;
        Debug.Log(currentSelection);
    }

    public void MoveDown() {
        currentSelection = (currentSelection + 1) % options.Count; 
        Debug.Log(currentSelection);
    }

    public void MoveLeft() {
        options[currentSelection].MoveLeft();
    }

    public void MoveRight() {
        options[currentSelection].MoveRight();
    }

    public void Select() {
        options[currentSelection].Select();
    }

    public void CloseUI() {
        Debug.Log("Close");
        foreach(ISelectable option in options) {
            option.Confirm();
        }
        playerUIController.SetActive(false);
        uiManager.ActivateGameUI(playerIndex);
    }
}
