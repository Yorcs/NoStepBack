using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingGun : AbstractWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        doesFreeze = true;
        statusDuration = 3;
        numBullets = 1;
        fireRate = 45;
        spread = 3;
        bulletSpeed = 12f;
        damage = 15;
    }
}
