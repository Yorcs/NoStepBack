using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public EquipmentFactory equipmentFactory;
    private Shop shop;
    private ShopUI shopUI;
    private float randomNumber;

    public void Start()
    {
        shopUI = ShopUI.instance;
        equipmentFactory = EquipmentFactory.instance;

    }

    public void UpdateUIImage(IEquipment equipment)
    {
        shop.SetEquipmentSprite(equipment.GetEquipmentImage());
    }

    public void CreateShopItem(Vector2 position){
        EquipmentType type;

        randomNumber = Random.Range(1, 10);

        if(randomNumber < 3){ //30% chance
            type = EquipmentType.WEAPON;
        }
        else {
            //TODO: Subweapon image is throwing a null.
            type = EquipmentType.SUBWEAPON;
        }

        int upgradeRanks = Random.Range(0,10);

        IEquipment newItem = equipmentFactory.CreateRandomEquipment(type, upgradeRanks, position);
        UpdateUIImage(newItem);
    }

    public Shop GetUI()
    {
        Shop shopPopup = shopUI.ShowPopup();
        return shopPopup;
    }

    public void CreatePopup()
    {
        if (shop == null)
        {
            shop = GetUI();
            shop.SetPosition(transform.position);
            CreateShopItem(transform.position);
        }
        shop.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        if(shop!= null)
        {
            shop.gameObject.SetActive(false);
        }
    }

    //public void Purchase(PlayerStatus money)
    //{
        //if the player's money is bigger or equal to the price, allow purchase.
    //}

    //method that can return price
}
