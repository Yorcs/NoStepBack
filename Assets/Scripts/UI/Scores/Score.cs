using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI weaponValueText;
    [SerializeField] private RectTransform weaponValueRect;

    [SerializeField] TextMeshProUGUI subweaponValueText;
    [SerializeField] private RectTransform subweaponValueRect;

    [SerializeField] TextMeshProUGUI currentMoneyText;
    [SerializeField] private RectTransform currentMoneyRect;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] private RectTransform totalScoreRect;

    [SerializeField] private Image weaponImage;
    [SerializeField] private RectTransform weaponTransform;
    [SerializeField] private Image subweaponImage;
    [SerializeField] private RectTransform subweaponTransform;
    private PlayerStatus status;
    private PlayerActions actions;
    private Weapon weapons;
    private AbstractSubweapon subweapon;

    // Start is called before the first frame update
    void Start()
    {
        weaponTransform = weaponImage.gameObject.GetComponent<RectTransform>();
        subweaponTransform = subweaponImage.gameObject.GetComponent<RectTransform>();
    }

    public void GetPlayerStatus(PlayerStatus status, PlayerActions actions){
        this.status = status;
        this.actions = actions;
    }

    public void UpdateWeaponScore(){
        weaponValueText.text = weapons.GetPrice().ToString();
    }

    public void UpdateSubweaponScore(){
        subweaponValueText.text = subweapon.GetPrice().ToString();
    }

    public void UpdateCurrentMoney(){
        currentMoneyText.text = status.GetMoney().ToString();
    }

    public void UpdateCurrentWeaponImage(){
        weaponImage.sprite = weapons.GetEquipmentImage();
    }

    public void UpdateCurrentSubweaponImage(){
        subweaponImage.sprite = subweapon.GetEquipmentImage();
    }

    public void SetTotalScore(){
        int total = weapons.GetPrice() + subweapon.GetPrice() + status.GetMoney();
        totalScoreText.text = total.ToString();
    }

    public void UpdateWeapon(){
        weapons = actions.GetWeapon();
    }

    public void UpdateSubweapon(){
        subweapon = actions.GetSubweapon();
    }
}
