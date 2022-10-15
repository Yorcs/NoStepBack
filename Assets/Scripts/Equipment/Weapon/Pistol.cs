using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Pistol : AbstractWeapon {

    private void Start() {
        numBullets = 1;
        fireRate = 60;
        spread = 2;
        bulletSpeed = 12f;
        damage = 10;
    }

}
