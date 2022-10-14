using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingGun : AbstractWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        stoppingTime = 10;
        numBullets = 1;
        bulletTime = 1;
        fireRate = 45;
        spread = 0;
        bulletSpeed = 12f;
        damage = 15;
    }
}
