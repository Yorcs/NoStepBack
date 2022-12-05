using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    WEAPON,
    SUBWEAPON,
    MOD,
}

public interface IEquipment {
    
    public EquipmentType GetEquipmentType();
    public int GetDamage();
    public float GetFireRate();

    public void Upgrade(int ranks);
    public Sprite GetEquipmentImage();

    public void DestroyEquipment(); 

}
