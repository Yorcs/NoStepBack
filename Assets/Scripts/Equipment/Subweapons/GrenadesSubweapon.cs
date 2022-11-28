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

    private void Update() {
        if(fireTimer > 0) {
            fireTimer -= Time.deltaTime;
        }
    }
    
    public override void UseSubweapon(Vector2 direction, int layer) {
        if(fireTimer <= 0) {
            SpawnGrenade(direction, layer);
            fireTimer = fireRate;
        }
    }


    private Grenade SpawnGrenade(Vector2 direction, int layer) {
        GameObject GO = Instantiate(grenadePrefab);
        GO.transform.position = transform.position;
        GO.layer = layer;

        Grenade newGrenade = GO.GetComponent<Grenade>();
        Assert.IsNotNull(newGrenade);

        newGrenade.SetFuse(fuse);
        newGrenade.SetDamage(damage);
        //Todo: No magic numbers
        newGrenade.SetVelocity(new Vector2(10, 5) * direction.x);

        return newGrenade;
    }

    

}
