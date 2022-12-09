using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    WEAPON,
    SUBWEAPON,
    MOD,
}

public enum Rarity {
    COMMON,
    UNCOMMON,
    RARE,
    LEGENDARY
}

public struct StatDisplay {
    public string text;
    public float value;
    public int upgrades;
    public StatDisplay(string text, float value, int upgrades) {
        this.text = text;
        this.value = value;
        this.upgrades = upgrades;
    }
}

public interface IEquipment {
    
    EquipmentType GetEquipmentType();
    int GetDamage();
    float GetFireRate();

    void Upgrade(int ranks);
    Sprite GetEquipmentImage();

    void DestroyEquipment(); 

    List<StatDisplay> GetStats();

    int GetPrice();

    Rarity GetRarity();

    void ShowWeapon();
    void HideWeapon();

}
