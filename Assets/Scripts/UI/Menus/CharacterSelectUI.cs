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
        options[currentSelection].OnHover(); 
        Debug.Log(currentSelection);
    }

    public void MoveUp() {
        options[currentSelection].OnHoverLeave();
        Debug.Log(currentSelection);
        if(currentSelection == 0) currentSelection = options.Count;
        currentSelection = (currentSelection - 1) % options.Count;
        options[currentSelection].OnHover();
        Debug.Log(currentSelection);
    }

    public void MoveDown() {
        options[currentSelection].OnHoverLeave();
        Debug.Log(currentSelection);
        currentSelection = (currentSelection + 1) % options.Count;
        options[currentSelection].OnHover(); 
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
        bool confirmed = true;
        foreach(ISelectable option in options) {
            bool optionConfirmed = option.Confirm();
            if(!optionConfirmed) {
                confirmed = false;
            }
        }
        if(confirmed) {
            playerUIController.SetActive(false);
            uiManager.ActivateGameUI(playerIndex);
        }
    }
}
