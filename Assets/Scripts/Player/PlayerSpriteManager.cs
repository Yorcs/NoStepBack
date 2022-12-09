using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSpriteManager : MonoBehaviour {
    public Sprite[] sprites;
    public RuntimeAnimatorController[] controllers;
    public List<Color> characterColors = new();
    private List<int> usedUpSprites = new();

    // public void OnPlayerJoined(PlayerInput input) {
    //     SpriteRenderer playerSprite = input.gameObject.GetComponent<SpriteRenderer>();
    //     Animator playerAnim = input.gameObject.GetComponent<Animator>();
    //     playerAnim.runtimeAnimatorController = controllers[spriteIndex];
    //     playerSprite.sprite = sprites[spriteIndex];
    //     spriteIndex ++;
    // }

    public void Start() {
        // characterColors.Add(new Color(200, 56, 255));
        // characterColors.Add(new Color(255, 107, 217));
        // characterColors.Add(new Color(255, 181, 66));
        // characterColors.Add(Color.green);
    }

    public int GetSpritesSize() {
        return sprites.Length;
    }

    public int GetNextIndex(int index) {
        int spriteIndex = index;
        bool spriteFound = false;
        while(!spriteFound) {
            spriteIndex = (spriteIndex + 1) % sprites.Length;
            spriteFound = !usedUpSprites.Contains(spriteIndex);
        }
        Debug.Log(spriteIndex);
        return spriteIndex;
    }

    public int GetPreviousIndex(int index) {
        int spriteIndex = index;
        bool spriteFound = false;
        while(!spriteFound) {
            spriteIndex -= 1;
            if(spriteIndex < 0) spriteIndex = sprites.Length - 1;
            spriteFound = !usedUpSprites.Contains(spriteIndex);
        }
        Debug.Log(spriteIndex);
        return spriteIndex;
    }

    public Sprite GetSprite(int index) {
        return sprites[index];
    }

    public bool IsSpriteAvailable(int index) {
        return !usedUpSprites.Contains(index);
    }

    public void ClaimSprite(int index) {
        usedUpSprites.Add(index);
    }

    public Color GetColor(int index) {
        return characterColors[index];
    }

    public RuntimeAnimatorController GetAnimator(int index) {
        return controllers[index];
    }


    
}
