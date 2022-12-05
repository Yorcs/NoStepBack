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
    [SerializeField] private List<TextMeshProUGUI> textObjects;
    // [SerializeField] private TextMeshProUGUI damageText;
    // [SerializeField] private TextMeshProUGUI fireRateText; 

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

    public void ShowPopup(EquipmentType type, List<StatDisplay> newStats, List<StatDisplay> currentStats) {
        popupImage.gameObject.SetActive(true);

        switch(type) {
            case EquipmentType.WEAPON:
                popupTransform.anchoredPosition = new Vector2(weaponTransform.anchoredPosition.x, popupTransform.anchoredPosition.y);
                // damageText.text = damage.ToString();
                // fireRateText.text = fireRate.ToString();
                break;
            case EquipmentType.SUBWEAPON:
                popupTransform.anchoredPosition = new Vector2(subweaponTransform.anchoredPosition.x, popupTransform.anchoredPosition.y);
                // damageText.text = damage.ToString();
                // fireRateText.text = "-";
                break;
            case EquipmentType.MOD:
                break;
        }

        foreach(TextMeshProUGUI textMesh in textObjects) {
            textMesh.text = "";
        }

        int textIndex = 0;
        foreach(StatDisplay stat in newStats) {

            StatDisplay toCompare = new("", 0, 0);
            foreach(StatDisplay currentStat in currentStats) {
                if(stat.text.Equals(currentStat.text)) toCompare = currentStat;
            }
            
            if(stat.upgrades != 0 || toCompare.upgrades != 0){
                textObjects[textIndex].text = stat.text + ": +" + stat.upgrades + "\n";
                Color newColor = Color.black;
                if(stat.upgrades > toCompare.upgrades) {
                    newColor = Color.blue;
                }
                if(stat.upgrades < toCompare.upgrades) {
                    newColor = Color.red;
                }

                textObjects[textIndex].color = newColor;
                textIndex++;
            }
            
        }
    
        
    }

    public void HidePopup() {
        popupImage.gameObject.SetActive(false);
    }
}
