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
    [SerializeField] PlayerHealthUI healthBar;
    

    public void Start() {
        spriteManager = GetComponentInParent<PlayerSpriteManager>();
        Assert.IsNotNull(spriteManager);

        selectionImage = GetComponent<Image>();
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);

        Assert.IsNotNull(healthBar);
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
        spriteIndex = spriteManager.GetPreviousIndex(spriteIndex);
        // if(spriteIndex == 0) spriteIndex = spriteManager.GetSpritesSize();
        // spriteIndex = (spriteIndex - 1) % spriteManager.GetSpritesSize();
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);
    }

    public void MoveRight() {
        spriteIndex = spriteManager.GetNextIndex(spriteIndex);
        selectionImage.sprite = spriteManager.GetSprite(spriteIndex);
    }

    public void Select() {
        Debug.Log("Can't Select");
    }

    //TODO: Set HealthUI color
    //ensure no duplicate players
    public bool Confirm() {
        bool available = spriteManager.IsSpriteAvailable(spriteIndex);
        if(available) {
            playerAnimator.runtimeAnimatorController = spriteManager.GetAnimator(spriteIndex);
            spriteManager.ClaimSprite(spriteIndex);
            healthBar.SetHeartColor(spriteManager.GetColor(spriteIndex));
        }
        return available;
    }
    
}
