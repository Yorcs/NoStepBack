using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour {
    bool active = false;
    
    private CharacterSelectUI characterSelect;
    private CutsceneUI cutscenePlayer;


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
        if(context.started || context.canceled) return;
        cutscenePlayer.AdvanceCutscene();
        if(!active) return;

        characterSelect.Select();
    }

    public void SetUI(CharacterSelectUI characterSelect, CutsceneUI cutscenePlayer) {
        this.characterSelect = characterSelect;
        characterSelect.Setup(this);
        this.cutscenePlayer = cutscenePlayer;
    }

    public void SetActive(bool active) {
        this.active = active;
    }
}
