using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupFactory : MonoBehaviour {

    public GameObject[] weapons;

    [SerializeField] GameObject pickup;

    private void Start() {

        Assert.IsNotNull(weapons);
        weapons = Resources.LoadAll<GameObject>("Weapons");

        Debug.Log(weapons.Length);
    }

    public void CreatePickup(Vector2 position) {
        //Todo: less hard code
        //Todo: Drop chances/randomness
        GameObject GO = Instantiate(pickup);
        pickup.transform.position = position;
        Pickup newPickup = GO.GetComponent<Pickup>();

        Assert.IsNotNull(newPickup);

        int itemIndex = Random.Range(0, weapons.Length);

        GameObject item = Instantiate(weapons[itemIndex]);
        item.transform.position = position;

        newPickup.SetItem(item);
    }
}
