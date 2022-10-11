using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractSubweapon : MonoBehaviour, IEquipment {

    private equipmentType equipType = equipmentType.SUBWEAPON;
    [SerializeField] protected int fireRate;
    protected int fireTimer = 0;

    [SerializeField] protected int damage;

    //void fixedUpdate() {
    //    if(fireTimer < fireRate) {
    //        fireTimer++;
    //    }
    //}


    public equipmentType GetEquipmentType() {
        return equipType;
    }
}
