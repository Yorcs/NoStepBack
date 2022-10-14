using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : AbstractWeapon
{
    private void Start()
    {
        numBullets = 5;
        bulletTime = 1;
        fireRate = 90;
        spread = 30;
        bulletSpeed = 12f;
        damage = 5;
    }
}
