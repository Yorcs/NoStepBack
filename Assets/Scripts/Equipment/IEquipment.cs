using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum equipmentType {
    WEAPON,
    SUBWEAPON,
    MOD,
}

public interface IEquipment {
    
    public equipmentType GetEquipmentType();

}
