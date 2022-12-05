using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum GrenadeUpgradeType {
    DAMAGE,
    FIRERATE,
    RADIUS,
}

public class GrenadesSubweapon : AbstractSubweapon {
    [SerializeField] private GameObject grenadePrefab;

    [SerializeField] private float fuse;
    [SerializeField] private float radius;
    [SerializeField] private float radiusRankStep;
    private int radiusRanks;

    [SerializeField] private float fireRate;
    [SerializeField] private float fireRateRankStep;
    private int fireRateRanks;

    [SerializeField] private Vector2 throwStrength = new Vector2 (10,5);
    private float fireTimer = 0;


    private void Start() {
        subweaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
        Assert.IsNotNull(grenadePrefab);
    }

    public override void Upgrade(int ranks) {
        for(int i = 0; i < ranks; i++) {
            GrenadeUpgradeType upgrade = (GrenadeUpgradeType)Random.Range(0, System.Enum.GetValues(typeof(GrenadeUpgradeType)).Length);

            switch(upgrade) {
            case GrenadeUpgradeType.DAMAGE: 
                damageRanks++;
                break;
            case GrenadeUpgradeType.FIRERATE: 
                fireRateRanks++;
                break;
            case GrenadeUpgradeType.RADIUS: 
                radiusRanks++;
                break;
            default: break;
            }
        }

        damage += damageRanks * damageRankStep;
        fireRate += fireRateRanks * fireRateRankStep;
        radius += radiusRanks * radiusRankStep;
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
        newGrenade.SetVelocity(throwStrength * direction);

        return newGrenade;
    }

    

}
