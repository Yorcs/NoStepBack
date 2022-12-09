using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractSubweapon : MonoBehaviour, IEquipment {
    private EquipmentType equipType = EquipmentType.SUBWEAPON;
    private SpriteRenderer subweaponRenderer;
    protected int totalRanks;

    [SerializeField] protected int damage;
    [SerializeField] protected int damageRankStep;
    protected int damageRanks;

    [SerializeField] protected int weaponValue;
    [SerializeField] protected int rankValueStep;

    private void Awake() {
        subweaponRenderer = gameObject.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
    }
    
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

    public int GetPrice() {
        return weaponValue;
    }

    public Rarity GetRarity() {
        if(totalRanks <= 4) {
            return Rarity.COMMON;
        }
        if(totalRanks <= 9) {
            return Rarity.UNCOMMON;
        }
        if(totalRanks < 13) {
            return Rarity.RARE;
        }
        return Rarity.LEGENDARY;
    }

    public void ShowWeapon() {
        subweaponRenderer.enabled = true;
    }
    public void HideWeapon() {
        subweaponRenderer.enabled = false;
    }
}
