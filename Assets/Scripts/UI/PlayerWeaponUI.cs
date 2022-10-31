using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUI : MonoBehaviour {
    
    [SerializeField] private Image weaponImage;
    private RectTransform weaponTransform;
    [SerializeField] private Image subWeaponImage;
    private RectTransform subweaponTransform;

    [SerializeField] private Image popupImage;
    private RectTransform popupTransform;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI fireRateText; 

    private void Start() {
        weaponTransform = weaponImage.gameObject.GetComponent<RectTransform>();
        subweaponTransform = subWeaponImage.gameObject.GetComponent<RectTransform>();
        popupTransform = popupImage.gameObject.GetComponent<RectTransform>();
    }

    public void SetWeapon(Sprite weaponSprite) {
        weaponImage.sprite = weaponSprite;
    }

    public void SetSubweapon(Sprite subweaponSprite) {
        subWeaponImage.gameObject.SetActive(true);
        subWeaponImage.sprite = subweaponSprite;
    }

    public void ShowPopup(EquipmentType type, int damage, float fireRate) {
        popupImage.gameObject.SetActive(true);
        switch(type) {
            case EquipmentType.WEAPON:
                popupTransform.anchoredPosition = new Vector2(weaponTransform.anchoredPosition.x, popupTransform.anchoredPosition.y);
                damageText.text = damage.ToString();
                fireRateText.text = fireRate.ToString();
                break;
            case EquipmentType.SUBWEAPON:
                popupTransform.anchoredPosition = new Vector2(subweaponTransform.anchoredPosition.x, popupTransform.anchoredPosition.y);
                damageText.text = damage.ToString();
                fireRateText.text = "-";
                break;
            case EquipmentType.MOD:
                break;
        }
        
    }

    public void HidePopup() {
        Debug.Log("Hide");
        popupImage.gameObject.SetActive(false);
    }
}
