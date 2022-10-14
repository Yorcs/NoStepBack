using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Pistol : AbstractWeapon {

    private void Start() {
        stoppingTime = 1;
        numBullets = 1;
        bulletTime = 1;
        fireRate = 60;
        spread = 0;
        bulletSpeed = 12f;
        damage = 10;
    }

}
