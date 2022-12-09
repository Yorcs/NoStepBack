using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour, ISelectable {
    [SerializeField] private List<Image> arrows = new();
    private StartingWeaponManager weaponManager;

    private PlayerActions playerActions;

    private int spriteIndex = 0;

    private Image selectionImage;

    public void Start() {
        weaponManager = GetComponentInParent<StartingWeaponManager>();
        Assert.IsNotNull(weaponManager);

        selectionImage = GetComponent<Image>();
        selectionImage.sprite = weaponManager.GetSprite(spriteIndex);
    }

    public void Setup(PlayerUIController player) {
        playerActions = player.gameObject.GetComponent<PlayerActions>();
    }

    public void OnHover() {
        foreach(Image arrow in arrows) {
            arrow.color = Color.red;
        }
    }

    public void OnHoverLeave() {
        foreach(Image arrow in arrows) {
            arrow.color = Color.white;
        }
    }
    
    public void MoveLeft() {
        if(spriteIndex == 0) spriteIndex = weaponManager.GetSpritesSize();
        spriteIndex = (spriteIndex - 1) % weaponManager.GetSpritesSize();
        selectionImage.sprite = weaponManager.GetSprite(spriteIndex);
    }

    public void MoveRight() {
        spriteIndex = (spriteIndex + 1) % weaponManager.GetSpritesSize();
        selectionImage.sprite = weaponManager.GetSprite(spriteIndex);
    }

    public void Select() {
        Debug.Log("Can't Select");
    }

    //TODO: Set HealthUI color
    //ensure no duplicate players
    public bool Confirm() {
        playerActions.SetWeapon(weaponManager.GetWeapon(spriteIndex));
        return true;
    }
}
