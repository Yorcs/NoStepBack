using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : AbstractWeapon
{
    private void Start()
    {
        stoppingTime = 1;
        numBullets = 5;
        bulletTime = 1;
        fireRate = 90;
        spread = 30;
        bulletSpeed = 12f;
        damage = 5;
    }
}
