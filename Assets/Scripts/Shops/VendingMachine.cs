using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public EquipmentFactory equipmentFactory;
    private IEquipment item;
    private Shop shop;
    private ShopUI shopUI;
    private float randomNumber;

    public void Start()
    {
        shopUI = ShopUI.instance;
        equipmentFactory = EquipmentFactory.instance;
        CreateShopItem(transform.position);
    }

    public void UpdateUIImage()
    {
        Color color = EquipmentFactory.instance.GetRarityColor(item.GetRarity());
        shop.SetEquipmentSprite(item.GetEquipmentImage(), color);
    }

    public void UpdateUIPrice()
    {
        shop.SetPrice(item.GetPrice());
    }

    public void CreateShopItem(Vector2 position){
        EquipmentType type;

        randomNumber = Random.Range(1, 10);

        if(randomNumber < 5){ //30% chance
            type = EquipmentType.WEAPON;
        }
        else {
            //TODO: Subweapon image is throwing a null.
            type = EquipmentType.SUBWEAPON;
        }

        int level = GameFlowManager.instance.GetLevel();
        int minRanks = level * 3;
        int maxRanks = (level + 1) * 5;
        int upgradeRanks = Random.Range(minRanks,maxRanks);

        IEquipment newItem = equipmentFactory.CreateRandomEquipment(type, upgradeRanks, position);
        newItem.HideWeapon();
        item = newItem;
        
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
            UpdateUIImage();
            UpdateUIPrice();
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

    public void Purchase(PlayerActions playerActions, PlayerStatus playerStatus)
    {
        int value = item.GetPrice();
        if(playerStatus.GetMoney() >= value) {
            playerStatus.SpendMoney(value);
            item.ShowWeapon();
            playerActions.EquipItem(item);
            CreateShopItem(transform.position);
            UpdateUIImage();
            UpdateUIPrice();

            FindObjectOfType<AudioManager>().Play("Purchase");
        }
    }

    public EquipmentType GetEquipmentType() {
        return item.GetEquipmentType();
    }

    public List<StatDisplay> GetStats() {
        return item.GetStats();
    }
}
