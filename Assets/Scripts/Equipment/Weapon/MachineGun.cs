using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MachineGun : AbstractWeapon {

    void Start() {
        stoppingTime = 1;
        numBullets = 1;
        bulletTime = 1;
        fireRate = 20;
        spread = 0;
        bulletSpeed = 12f;
        damage = 5;
    }
}
