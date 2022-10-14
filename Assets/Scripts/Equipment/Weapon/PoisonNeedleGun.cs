using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonNeedleGun : AbstractWeapon
{
    private void Start()
    {
        stoppingTime = 1;
        numBullets = 1;
        bulletTime = 3;
        fireRate = 30;
        spread = 0;
        bulletSpeed = 12f;
        damage = 2;
    }
}
