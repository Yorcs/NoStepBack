using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupFactory : MonoBehaviour {

    public GameObject[] weapons;
    

    [SerializeField] GameObject pickup;
    private float randomNumber;

    private void Start() {

        Assert.IsNotNull(weapons);
        weapons = Resources.LoadAll<GameObject>("Weapons");

        Debug.Log(weapons.Length);
    }

    public void CreatePickup(Vector2 position) {
        //Todo: less hard code
        GameObject GO = Instantiate(pickup);
        pickup.transform.position = position;
        Pickup newPickup = GO.GetComponent<Pickup>();

        Assert.IsNotNull(newPickup);

        randomNumber = Random.Range(1,10);
        if(randomNumber < 3) //30% chance
        {
            int itemIndex = Random.Range(0, weapons.Length);
            GameObject item = Instantiate(weapons[itemIndex]);           
            item.transform.position = position;
            newPickup.SetItem(item);  
        }
        else if(randomNumber < 5) //50% chance
        {

        }
        else if(randomNumber < 8) // 80% chance
        {
        }
        
    }
}
