using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GrenadesSubweapon : AbstractSubweapon {
    [SerializeField] GameObject grenadePrefab;

    private int fuse;
    private int fuseTimer;
    private float radius;


    private void Start() {
        Assert.IsNotNull(grenadePrefab);

        fireRate = 5;
        damage = 150;
        fuse = 120;
        radius = 5;
    }

    public override void UseSubweapon() {
        SpawnGrenade();
    }

    private Grenade SpawnGrenade() {
        GameObject GO = Instantiate(grenadePrefab);
        GO.transform.position = transform.position;

        Grenade newGrenade = GO.GetComponent<Grenade>();
        Assert.IsNotNull(newGrenade);

        newGrenade.SetFuse(fuse);
        newGrenade.SetDamage(damage);
        newGrenade.SetRadius(radius);
        //Todo: No magic numbers
        newGrenade.SetVelocity(new Vector2(10, 5));

        return newGrenade;
    }

    

}
