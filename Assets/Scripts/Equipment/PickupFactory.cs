using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupFactory : MonoBehaviour {

    public GameObject[] weapons;
    public GameObject[] subWeapons;
    public GameObject[] mods;
    

    [SerializeField] GameObject pickup;
    private float randomNumber;

    private void Start() {
        Assert.IsNotNull(weapons);
        weapons = Resources.LoadAll<GameObject>("Weapons");
        // subWeapons = Resources.LoadAll<GameObject>("SubWeapons");
        // mods = Resources.LoadAll<GameObject>("Mods");

        Debug.Log(weapons.Length);
    }

    public void CreatePickup(Vector2 position) {
        //Todo: less hard code
        //Todo: implement more item variations
        GameObject GO = Instantiate(pickup);
        pickup.transform.position = position;
        Pickup newPickup = GO.GetComponent<Pickup>();

        Assert.IsNotNull(newPickup);

        randomNumber = Random.Range(1,10);
        if(randomNumber < 11) //30% chance
        {
            int itemIndex = Random.Range(0, weapons.Length);
            GameObject item = Instantiate(weapons[itemIndex]);        
            item.transform.position = position;
            IEquipment newEquip = item.GetComponent<IEquipment>();
            Assert.IsNotNull(newEquip);
            newPickup.SetItem(newEquip);  
        }
        else if(randomNumber < 5) //50% chance
        {
            //SubWeapons
            // int itemIndex = Random.Range(0, subWeapons.Length);
            // GameObject item = Instantiate(subWeapons[itemIndex]);           
            // item.transform.position = position;
            // newPickup.SetItem(item);  
        }
        else if(randomNumber < 8) // 80% chance
        {
            //Mods
            // int itemIndex = Random.Range(0, mods.Length);
            // GameObject item = Instantiate(mods[itemIndex]);           
            // item.transform.position = position;
            // newPickup.SetItem(item);  
        }
        
    }
}
