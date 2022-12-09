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

    public void SetPlayerStatus(PlayerStatus status, PlayerActions actions){
        this.status = status;
        this.actions = actions;
    }
    
    public void DisplayScore() {
        UpdateWeapon();
        UpdateCurrentWeaponImage();
        UpdateWeaponScore();

        if(subweapon != null) {
            UpdateSubweapon();
            UpdateCurrentSubweaponImage();
            UpdateSubweaponScore();
        }

        UpdateCurrentMoney();

        UpdateTotalScore();
    }


    private void UpdateWeaponScore(){
        weaponValueText.text = weapons.GetPrice().ToString();
    }

    private void UpdateSubweaponScore(){
        subweaponValueText.text = subweapon.GetPrice().ToString();
    }

    private void UpdateCurrentMoney(){
        currentMoneyText.text = status.GetMoney().ToString();
    }

    private void UpdateCurrentWeaponImage(){
        weaponImage.sprite = weapons.GetEquipmentImage();
    }

    private void UpdateCurrentSubweaponImage(){
        subweaponImage.sprite = subweapon.GetEquipmentImage();
    }

    private void UpdateTotalScore(){
        int total = weapons.GetPrice() + status.GetMoney();
        if(subweapon != null) total += subweapon.GetPrice();
        totalScoreText.text = total.ToString();
    }

    private void UpdateWeapon(){
        weapons = actions.GetWeapon();
    }

    private void UpdateSubweapon(){
        subweapon = actions.GetSubweapon();
    }

    public void ClosePanel() {
        GameFlowManager.instance.CloseScorePanel(this);
    }
}
