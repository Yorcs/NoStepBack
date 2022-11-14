using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ReadyButton : MonoBehaviour, ISelectable {
    private CharacterSelectUI characterSelect;

    public void Setup(PlayerUIController player) {
        characterSelect = GetComponentInParent<CharacterSelectUI>();
        Assert.IsNotNull(characterSelect);
    }

    public void MoveLeft() {
        Debug.Log("Cannot move left or right");
    }

    public void MoveRight() {
        Debug.Log("Cannot move left or right");
    }

    public void Select() {
        characterSelect.CloseUI();
    }

    public void Confirm() {}
}
