using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUI : MonoBehaviour {
    
    [SerializeField] Image weaponImage;
    [SerializeField] Image subWeaponImage;

    public void SetWeapon(Sprite weaponSprite) {
        weaponImage.sprite = weaponSprite;
    }

    public void SetSubweapon(Sprite subweaponSprite) {
        subWeaponImage.gameObject.SetActive(true);
        subWeaponImage.sprite = subweaponSprite;
    }
}
