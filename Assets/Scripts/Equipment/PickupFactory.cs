using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupFactory : MonoBehaviour {
    public static PickupFactory instance;

    EquipmentFactory equipmentFactory;


    [SerializeField] GameObject pickup;
    [SerializeField] GameObject money;
    private float randomNumber;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        equipmentFactory = EquipmentFactory.instance;
    }

    public void CreatePickupFromEquipment(Vector2 position, IEquipment item, Transform itemTransform) {
        Pickup newPickup = CreatePickupObject(position);
        newPickup.SetItem(item);
        itemTransform.position = newPickup.transform.position;
        itemTransform.rotation = Quaternion.identity;
        itemTransform.SetParent(newPickup.transform);
        newPickup.SetItem(item);
    }

    public void CreatePickup(Vector2 position) {
        //Todo: less hard code
        //Todo: implement more item variations
        //Todo: Drop tables probably
        EquipmentType type;

        randomNumber = Random.Range(1, 10);

        if (randomNumber < 4) { //40% chance
            type = EquipmentType.WEAPON;
        }
        else if (randomNumber < 8) { //30% chance
            type = EquipmentType.SUBWEAPON;
        }
        else {
            return;
        }

        int level = GameFlowManager.instance.GetLevel();
        int minRanks = (level - 1) * 3;
        int maxRanks = level * 5;
        int upgradeRanks = Random.Range(minRanks,maxRanks);

        Pickup newPickup = CreatePickupObject(position);
        IEquipment newEquip = equipmentFactory.CreateRandomEquipment(type, upgradeRanks, position);
        
        newPickup.SetItem(newEquip);
    }

    private Pickup CreatePickupObject(Vector2 position)
    {
        GameObject GO = Instantiate(pickup, position, Quaternion.identity);
        Pickup newPickup = GO.GetComponent<Pickup>();

        Assert.IsNotNull(newPickup);
        return newPickup;
    }

    public void CreateMoney(Vector2 position)
    {
        int randomNumber = Random.Range(1, 10);
        for(int i = 0; i < randomNumber; i++)
        {
            Money money = CreateMoneyObject(position);
            Vector2 force = new Vector2(Random.Range(-10, 10), Random.Range(0, 8));
            money.SetForce(force);

            int level = GameFlowManager.instance.GetLevel();
            int minimumMoney = level;
            int maximumMoney = level * 2;

            money.SetMoney(Random.Range(minimumMoney, maximumMoney));
        }
    }

    private Money CreateMoneyObject(Vector2 position)
    {
        GameObject GO = Instantiate(money, position, Quaternion.identity);
        Money newMoney = GO.GetComponent<Money>();

        Assert.IsNotNull(newMoney);
        return newMoney;
    }

}
