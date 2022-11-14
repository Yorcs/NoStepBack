using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour, ISelectable {
    [SerializeField] private List<Image> arrows = new();
    private PlayerSpriteManager spriteManager;

    private Animator playerAnimator;

    private int spriteIndex = 0;

    private Image selectionImage;

    public void Start() {
        spriteManager = GetComponentInParent<PlayerSpriteManager>();
        Assert.IsNotNull(spriteManager);

        selectionImage = GetComponent<Image>();
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);
    }

    public void Setup(PlayerUIController player) {
        playerAnimator = player.gameObject.GetComponent<Animator>();
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
        if(spriteIndex == 0) spriteIndex = spriteManager.GetSpritesSize();
        spriteIndex = (spriteIndex - 1) % spriteManager.GetSpritesSize();
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);
    }

    public void MoveRight() {
        spriteIndex = (spriteIndex + 1) % spriteManager.GetSpritesSize();
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);
    }

    public void Select() {
        Debug.Log("Can't Select");
    }

    //TODO: Set HealthUI color
    //ensure no duplicate players
    public void Confirm() {
        playerAnimator.runtimeAnimatorController = spriteManager.GetAnimator(spriteIndex);
    }
    
}
