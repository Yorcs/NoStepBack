using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Pistol : AbstractWeapon {

    private void Start() {
        fireRate = 60;

        bulletSpeed = 12f;
        damage = 10;
    }

}