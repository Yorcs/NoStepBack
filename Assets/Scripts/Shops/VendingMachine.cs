using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public IEquipment equipment;
    private Shop shop;
    private ShopUI shopUI;

    public void Start()
    {
        shopUI = ShopUI.instance;
    }

    public void UpdateUIImage()
    {
        shop.SetEquipmentSprite(equipment.GetEquipmentImage());
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
