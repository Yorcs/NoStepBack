using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractSubweapon : MonoBehaviour, IEquipment {
    private equipmentType equipType = equipmentType.SUBWEAPON;
    protected SpriteRenderer subweaponRenderer;
    

    [SerializeField] protected int damage;


    //void fixedUpdate() {
    //    if(fireTimer < fireRate) {
    //        fireTimer++;
    //    }
    //}

    public abstract void UseSubweapon();


    public equipmentType GetEquipmentType() {
        return equipType;
    }

    public Sprite GetSubweaponImage() {
        return subweaponRenderer.sprite;
    }
}
