using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public GameObject item;

    public GameObject GetItem() {
        return item;
    }

    public void SetItem(GameObject item) {
        this.item = item;
    }
}
