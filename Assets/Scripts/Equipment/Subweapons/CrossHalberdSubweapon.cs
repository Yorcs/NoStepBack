using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum HalberdUpgradeType {
    DAMAGE,
    SPEED,
    PENETRATION,
}

public class CrossHalberdSubweapon : AbstractSubweapon {
    [SerializeField] private GameObject crossHalberdPrefab;

    [SerializeField] private float speed;
    [SerializeField] private int speedRankStep;
    private int speedRanks;

    [SerializeField] private int penetration;
    [SerializeField] private int penetrationRankStep;
    private int penetrationRanks;

    [SerializeField] private float returnTimer;

    private bool weaponReturned = true;


    // Start is called before the first frame update
    void Start() {
        Assert.IsNotNull(crossHalberdPrefab);
    }

    public override void Upgrade(int ranks) {
        for(int i = 0; i < ranks; i++) {
            HalberdUpgradeType upgrade = (HalberdUpgradeType)Random.Range(0, System.Enum.GetValues(typeof(HalberdUpgradeType)).Length);

            switch(upgrade) {
            case HalberdUpgradeType.DAMAGE: 
                damageRanks++;
                break;
            case HalberdUpgradeType.SPEED: 
                speedRanks++;
                break;
            case HalberdUpgradeType.PENETRATION: 
                penetrationRanks++;
                break;
            default: break;
            }
        }

        totalRanks = ranks;

        damage += damageRanks * damageRankStep;
        speed += speedRanks * speedRankStep;
        penetration += penetrationRanks * penetrationRankStep;
        weaponValue += ranks * rankValueStep;
    }
    
    public override void UseSubweapon(Vector2 direction, int layer) {
        if(weaponReturned) {
            SpawnHalberd(direction, layer);
        }
    }

    private CrossHalberd SpawnHalberd(Vector2 direction, int layer) {
        //Play the weapon audio when it spawns to enhance the sound of the throw
        var audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying) audioSource.Play();

        GameObject GO = Instantiate(crossHalberdPrefab);
        GO.transform.position = transform.position;
        GO.layer = layer;

        CrossHalberd newHalberd = GO.GetComponent<CrossHalberd>();
        Assert.IsNotNull(newHalberd);

        newHalberd.SetDamage(damage);
        newHalberd.SetSpeed(speed);
        newHalberd.SetDirection(direction);
        newHalberd.SetPenetration(penetration);
        newHalberd.SetReturnTimer(returnTimer);
        newHalberd.SetReturnTarget(this);

        weaponReturned = false;
        return newHalberd;
    }

    public override List<StatDisplay> GetStats() {
        List<StatDisplay> stats = new()
        {
            new StatDisplay("Damage", damage, damageRanks),
            new StatDisplay("Speed", speed, speedRanks),
            new StatDisplay("Pierce", penetration, penetrationRanks)
        };

        return stats;
    }

    public void ReturnHalberd() {
        weaponReturned = true;
    }
}
