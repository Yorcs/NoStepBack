using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpriteManager : MonoBehaviour {
    public Sprite[] sprites;
    private int spriteIndex;

    public void OnPlayerJoined(PlayerInput input) {
        SpriteRenderer playerSprite = input.gameObject.GetComponent<SpriteRenderer>();

        playerSprite.sprite = sprites[spriteIndex];
        spriteIndex ++;
    }
    
}
