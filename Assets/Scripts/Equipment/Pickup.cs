using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public IEquipment equipment;

    public IEquipment GetItem() {
        return equipment;
    }

    public void SetItem(IEquipment equipment) {
        this.equipment = equipment;
    }

    public EquipmentType GetEquipmentType() {
        return equipment.GetEquipmentType();
    }

    public int GetDamage() {
        return equipment.GetDamage();
    }

    public float GetFireRate() {
        return equipment.GetFireRate();
    }
}
