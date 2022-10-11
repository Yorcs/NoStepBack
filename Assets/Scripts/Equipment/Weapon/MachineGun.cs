using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MachineGun : AbstractWeapon {

    void Start() {
        fireRate = 20;

        bulletSpeed = 12f;
        damage = 5;
    }
}
