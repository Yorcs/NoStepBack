using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonNeedleGun : AbstractWeapon
{
    private void Start()
    {
        doesPoison = true;
        statusDuration = 3;
        statusDamage = 15;
        numBullets = 1;
        fireRate = 30;
        spread = 0;
        bulletSpeed = 12f;
        damage = 5;
    }
}
