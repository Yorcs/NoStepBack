using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GrenadesSubweapon : AbstractSubweapon {
    [SerializeField] private GameObject grenadePrefab;

    [SerializeField] private float fuse;
    [SerializeField] private float radius;

    [SerializeField] private float fireRate;
    private float fireTimer = 0;


    private void Start() {
        subweaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
        Assert.IsNotNull(grenadePrefab);
    }

    public override void UseSubweapon() {
        if(fireTimer <= 0) {
            SpawnGrenade();
            fireTimer = fireRate;
        }
    }

    private void Update() {
        if(fireTimer > 0) {
            fireTimer -= Time.deltaTime;
        }
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
