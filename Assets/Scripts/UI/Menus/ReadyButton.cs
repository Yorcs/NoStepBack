using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour, ISelectable {
    private Image readyImage;
    private CharacterSelectUI characterSelect;

    private void Start() {
        readyImage = GetComponent<Image>();
        Assert.IsNotNull(readyImage);
    }

    public void Setup(PlayerUIController player) {
        characterSelect = GetComponentInParent<CharacterSelectUI>();
        Assert.IsNotNull(characterSelect);
    }

    public void OnHover() {
        readyImage.color = Color.red;
    }

    public void OnHoverLeave() {
        readyImage.color = Color.white;
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

    public bool Confirm() { return true;}
}
