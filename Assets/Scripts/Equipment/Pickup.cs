using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public IEquipment equipment;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public IEquipment GetItem() {
        return equipment;
    }

    public void SetItem(IEquipment equipment) {
        this.equipment = equipment;
        SetRarityColor(equipment);
    }

    public void SetRarityColor(IEquipment equipment) {
        Color color = EquipmentFactory.instance.GetRarityColor(equipment.GetRarity());
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    public EquipmentType GetEquipmentType() {
        return equipment.GetEquipmentType();
    }

    public List<StatDisplay> GetStats() {
        return equipment.GetStats();
    }

    public int GetDamage() {
        return equipment.GetDamage();
    }

    public float GetFireRate() {
        return equipment.GetFireRate();
    }

    // private void OnBecameInvisible() {
    //     Destroy(gameObject);
    // }
}
