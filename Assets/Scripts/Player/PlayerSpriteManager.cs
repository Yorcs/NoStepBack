using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerSpriteManager : MonoBehaviour {
    public Sprite[] sprites;
    public AnimatorController[] controllers;
    private int spriteIndex;

    // public void OnPlayerJoined(PlayerInput input) {
    //     SpriteRenderer playerSprite = input.gameObject.GetComponent<SpriteRenderer>();
    //     Animator playerAnim = input.gameObject.GetComponent<Animator>();
    //     playerAnim.runtimeAnimatorController = controllers[spriteIndex];
    //     playerSprite.sprite = sprites[spriteIndex];
    //     spriteIndex ++;
    // }

    public int GetSpritesSize() {
        return sprites.Length;
    }

    public Sprite GetSprite(int index) {
        return sprites[index];
    }

    public AnimatorController GetAnimator(int index) {
        return controllers[index];
    }


    
}
