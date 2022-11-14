using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeaponManager : MonoBehaviour {
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] weapons;
    
    public int GetSpritesSize() {
        return sprites.Length;
    }

    public Sprite GetSprite(int index) {
        return sprites[index];
    }

    public GameObject GetWeapon(int index) {
        return weapons[index];
    }
}
