using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour {
    public List<ISelectable> options = new();

    public int currentSelection = 0;

    public void Setup(PlayerUIController player) {
        options.AddRange(GetComponentsInChildren<ISelectable>());

        foreach (ISelectable option in options) {
            option.Setup(player);
        }
    }

    public void MoveUp() {
        currentSelection = (currentSelection - 1) % options.Count;
    }

    public void MoveDown() {
        currentSelection = (currentSelection + 1) % options.Count; 
    }

    public void MoveLeft() {
        options[currentSelection].MoveLeft();
    }

    public void MoveRight() {
        options[currentSelection].MoveRight();
    }

    public void Confirm() {
        options[currentSelection].Select();
    }
}
