using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour {
    bool active = false;
    
    private CharacterSelectUI characterSelect;


    public void OnDirection(InputAction.CallbackContext context) {
        if(!active) return;
        if(context.started || context.canceled) return;

        Vector2 input = context.ReadValue<Vector2>();

        if(input.x < 0) characterSelect.MoveLeft();
        if(input.x > 0) characterSelect.MoveRight();
        if(input.y > 0) characterSelect.MoveUp();
        if(input.y < 0) characterSelect.MoveDown();
    }

    public void OnSelect(InputAction.CallbackContext context) {
        if(!active) return;
        if(context.started || context.canceled) return;

        characterSelect.Select();
    }

    public void SetUI(CharacterSelectUI characterSelect) {
        this.characterSelect = characterSelect;
        characterSelect.Setup(this);
    }

    public void SetActive(bool active) {
        this.active = active;
    }
}
