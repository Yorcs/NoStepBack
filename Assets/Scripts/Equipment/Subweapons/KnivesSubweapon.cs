using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum KnifeUpgradeType {
    DAMAGE,
    FIRERATE,
}

public class KnivesSubweapon : AbstractSubweapon
{
    [SerializeField] GameObject knivesPrefab;
    
    [SerializeField] private float speed = 20f;
    private float fireTimer = 0;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireRateRankStep;
    private int fireRateRanks;
    // private int uses = 1;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(knivesPrefab);
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
            default: break;
            }
        }

        damage += damageRanks * damageRankStep;
        fireRate += fireRateRanks * fireRateRankStep;
        weaponValue += ranks * rankValueStep;
    }

    //todo: Destroying this object causes problems with the player and seems to mess with subweapon pickups after that point
    public override void UseSubweapon(Vector2 direction, int layer)
    {
        if(fireTimer <= 0) {
            SpawnKnives(direction, layer);
            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
    }

    private Knives SpawnKnives(Vector2 direction, int layer)
    {
        GameObject GO = Instantiate(knivesPrefab);
        GO.transform.position = transform.position;
        GO.layer = layer;

        Knives newKnives = GO.GetComponent<Knives>();
        Assert.IsNotNull(newKnives);

        newKnives.SetDirection(direction);
        newKnives.SetSpeed(speed);
        newKnives.SetDamage(damage);

        return newKnives;
    }

    public override List<StatDisplay> GetStats() {
        List<StatDisplay> stats = new()
        {
            new StatDisplay("Damage", damage, damageRanks),
            new StatDisplay("Fire Rate", fireRate, fireRateRanks)
        };

        return stats;
    }

}
