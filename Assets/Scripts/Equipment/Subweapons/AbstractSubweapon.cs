using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractSubweapon : MonoBehaviour, IEquipment {
    private EquipmentType equipType = EquipmentType.SUBWEAPON;
    protected SpriteRenderer subweaponRenderer;
    

    [SerializeField] protected int damage;
    [SerializeField] protected int damageRankStep;
    protected int damageRanks;

    public abstract void UseSubweapon(Vector2 direction, int layer);


    public EquipmentType GetEquipmentType() {
        return equipType;
    }

    public Sprite GetEquipmentImage() {
        return subweaponRenderer.sprite;
    }

    public int GetDamage() {
        return damage;
    }

    //Todo: this feels bad
    public float GetFireRate() {
        return 0;
    }

    public abstract void Upgrade(int ranks);

    public abstract List<StatDisplay> GetStats();

    public void DestroyEquipment() {
        Destroy(gameObject);
    }
}
