using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupFactory : MonoBehaviour {

    public GameObject[] weapons;
    public GameObject[] subWeapons;
    public GameObject[] mods;


    [SerializeField] GameObject pickup;
    [SerializeField] GameObject money;
    private float randomNumber;

    private void Start() {
        Assert.IsNotNull(weapons);
        weapons = Resources.LoadAll<GameObject>("Weapons");
        subWeapons = Resources.LoadAll<GameObject>("SubWeapons");
        // mods = Resources.LoadAll<GameObject>("Mods");
    }

    public void CreatePickup(Vector2 position) {
        //Todo: less hard code
        //Todo: implement more item variations
        //Todo: Drop tables probably


        randomNumber = Random.Range(1, 10);
        if (randomNumber < 3) { //30% chance
            Pickup newPickup = CreatePickupObject(position);

            int itemIndex = Random.Range(0, weapons.Length);
            GameObject item = Instantiate(weapons[itemIndex], position, Quaternion.identity);
            IEquipment newEquip = item.GetComponent<IEquipment>();
            Assert.IsNotNull(newEquip);
            newPickup.SetItem(newEquip);
        }
        else if (randomNumber < 5) { //20% chance

            Pickup newPickup = CreatePickupObject(position);
            //SubWeapons
            int itemIndex = Random.Range(0, subWeapons.Length);
            GameObject item = Instantiate(subWeapons[itemIndex], position, Quaternion.identity);
            IEquipment newEquip = item.GetComponent<IEquipment>();
            Assert.IsNotNull(newEquip);
            newPickup.SetItem(newEquip);
        }
        else if (randomNumber < 8) { //40% chance

            //Mods
            // int itemIndex = Random.Range(0, mods.Length);
            // GameObject item = Instantiate(mods[itemIndex]);           
            // item.transform.position = position;
            // newPickup.SetItem(item);  
        }

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
            money.SetMoney(1);
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
