using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GrenadesSubweapon : AbstractSubweapon {
    [SerializeField] private GameObject grenadePrefab;

    private int fuse;
    [SerializeField] private int fuseTimer;
    [SerializeField] private float radius;


    private void Start() {
        Assert.IsNotNull(grenadePrefab);
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
