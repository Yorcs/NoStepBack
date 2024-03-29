using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum TargetType{
    ENEMY,
    PVP,
    PLAYER
}

public enum UpgradeType {
    DAMAGE,
    FIRERATE,
    CRITICAL,
    SPREAD,
    PENETRATION,
    STATUS
}

public class Weapon : MonoBehaviour, IEquipment {
    [SerializeField] private GameObject bullet;
    private EquipmentType equipType = EquipmentType.WEAPON;
    private SpriteRenderer weaponRenderer;
    [SerializeField] private Transform bulletSpawnPoint;
    private int totalRanks;
    [SerializeField] protected int numBullets = 1;

    private float fireTimer = 0;
    [SerializeField] protected float fireRate;
    [SerializeField] private float fireRateRankStep;
    // [SerializeField] private int fireRateWeight = 1;
    private int fireRateRanks;

    [SerializeField] protected int criticalChance;
    [SerializeField] private int criticalRankStep;
    // [SerializeField] private int criticalWeight = 1;
    private int criticalRanks;

    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int damage;
    [SerializeField] private int damageRankStep;
    // [SerializeField] private int damageWeight = 1;
    private int damageRanks;

    [SerializeField] protected float spread;
    [SerializeField] private float spreadRankStep;
    // [SerializeField] private int spreadWeight = 1;
    private int spreadRanks;

    [SerializeField] protected int penetration;
    [SerializeField] private int penetrationRankStep;
    // [SerializeField] private int penetrationWeight = 1;
    private int penetrationRanks;

    [SerializeField] private int weaponValue;
    [SerializeField] private int rankValueStep;

    //Status things to factor out later
    [SerializeField] protected bool doesFreeze, doesPoison = false;
    [SerializeField] protected float statusDuration = 0;
    [SerializeField] protected int statusDamage = 0;
    [SerializeField] private int statusRankStep;
    // [SerializeField] private int statusWeight = 1;
    private int statusRanks;

    private EnemyManager enemyManager;

    private void Awake() {
        weaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(weaponRenderer);
        Assert.IsNotNull(bulletSpawnPoint);
        enemyManager = EnemyManager.instance;
        Assert.IsNotNull(enemyManager);
    }

    public void Upgrade(int ranks) {
        for(int i = 0; i < ranks; i++) {
            UpgradeType upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
            
            // GetUpgrade(new int[] {damageWeight, fireRateWeight, penetrationWeight, criticalWeight, spreadWeight, statusWeight});

            switch(upgrade) {
            case UpgradeType.DAMAGE: 
                damageRanks++;
                break;
            case UpgradeType.FIRERATE: 
                fireRateRanks++;
                break;
            case UpgradeType.CRITICAL: 
                criticalRanks++;
                break;
            case UpgradeType.PENETRATION: 
                penetrationRanks++;
                break;
            case UpgradeType.SPREAD: 
                spreadRanks++;
                break;
            case UpgradeType.STATUS: 
                statusRanks++;
                break;
            default: break;
            }
        }

        totalRanks = ranks;

        damage += damageRanks * damageRankStep;
        fireRate += fireRateRanks * fireRateRankStep;
        criticalChance += criticalRanks * criticalRankStep;
        penetration += penetrationRanks * penetrationRankStep;
        spread += spreadRanks * spreadRankStep;
        statusDamage += statusRanks * statusRankStep;
        weaponValue += ranks * rankValueStep;

        if(statusRanks > 0) doesPoison = true;
    }

    public void Fire(Vector2 direction, TargetType targetType, int layer) {
        fireTimer += Time.deltaTime;
        Vector3 target = GetTarget(direction, targetType, layer);
        TrackTarget(target, direction);

        if((target - transform.position).magnitude > Screen.width) return;

        if (fireTimer >= fireRate) {
            fireTimer = 0;
            for(int i = 0; i < numBullets; i++){
                SpawnBullet(target, layer);
            }
        }
    }

    private Vector3 GetTarget(Vector2 direction, TargetType targetType, int layer) {
        Vector3 target = Vector3.positiveInfinity;
        switch(targetType) {
            case TargetType.ENEMY :
                target = enemyManager.FindClosestVisibleEnemy(bulletSpawnPoint.position, direction);
                break;
            case TargetType.PVP :
                target = PVPManager.instance.FindClosestVisibleOpponent(bulletSpawnPoint.position, direction, layer);
                break;
            case TargetType.PLAYER :
                target = enemyManager.FindClosestVisiblePlayer(bulletSpawnPoint.position, direction);
                break;
        }

        return target;
    }

    private void TrackTarget(Vector3 target, Vector2 direction) {
        transform.rotation = Quaternion.identity;
        if (IsEnemiesNear(target)) {
            transform.rotation = LookAt2D(target - transform.position);
            if (direction.x < 0) transform.Rotate(0, 0, 180);
        }
    }

    public bool IsEnemiesNear(Vector3 target) {
        return (target - transform.position).magnitude < Screen.width;   
    }

    private void SpawnBullet(Vector3 target, int layer) {
        //Play the weapon audio only when being fired
        var audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying) audioSource.Play();
        //issue: only works well for slow firing weapons

        //Stop the weapon audio when weapon stops firing
        //else if (audioSource.isPlaying) audioSource.Pause();

        GameObject GO = Instantiate(bullet);
        GO.transform.position = bulletSpawnPoint.position;
        GO.transform.rotation = LookAt2D(target - bulletSpawnPoint.position);
        GO.layer = layer;

        //randomizing spread
        var zSpread = Random.Range(-spread, spread);
        var spreadvec = new Vector3(0, 0, zSpread);

        //adding the spread
        // GO.transform.rotation = targetRotation;
        GO.transform.Rotate(spreadvec);

        AbstractProjectile newProjectile = GO.GetComponent<AbstractProjectile>();
        Assert.IsNotNull(newProjectile);

        newProjectile.SetDirection(Vector2.right);
        newProjectile.SetSpeed(bulletSpeed);
        newProjectile.SetDamage(damage);
        newProjectile.SetCritical(criticalChance);
        newProjectile.SetPenetration(penetration);
        
        //Status Stuff to refactor later
        newProjectile.SetIsPoisoned(doesPoison);
        newProjectile.SetIsFrozen(doesFreeze);
        newProjectile.SetStatusDuration(statusDuration);
        newProjectile.SetStatusDamage(statusDamage);
    }

    static Quaternion LookAt2D(Vector2 forward) {
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}

    public EquipmentType GetEquipmentType() {
        return equipType;
    }

    public Sprite GetEquipmentImage() {
        return weaponRenderer.sprite;
    }

    public int GetDamage() {
        return damage;
    }

    public float GetFireRate() {
        return fireRate;
    }

    public Vector2 GetBulletSpawn() {
        return bulletSpawnPoint.position;
    }

    // private int GetTotalWeight() {
    //     return damageRankWeight + criticalRankWeight + fireRateRankWeight +
    //         statusRankWeight + spreadRankWeight + penetrationRankWeight;
    // }

    public List<StatDisplay> GetStats() {
        List<StatDisplay> stats = new()
        {
            new StatDisplay("Damage", damage, damageRanks),
            new StatDisplay("Fire Rate", fireRate, fireRateRanks),
            new StatDisplay("Critical", criticalChance, criticalRanks),
            new StatDisplay("Accuracy", spread, spreadRanks),
            new StatDisplay("Pierce", penetration, penetrationRanks),
            new StatDisplay("Status", statusDamage, statusRanks)
        };

        return stats;
    }

    public void DestroyEquipment() {
        Destroy(gameObject);
    }

    public int GetPrice() {
        return weaponValue;
    }

    public Rarity GetRarity() {
        if(totalRanks <= 3) {
            return Rarity.COMMON;
        }
        if(totalRanks <= 9) {
            return Rarity.UNCOMMON;
        }
        if(totalRanks <= 13) {
            return Rarity.RARE;
        }
        return Rarity.LEGENDARY;
    }

    public void ShowWeapon() {
        weaponRenderer.enabled = true;
    }
    public void HideWeapon() {
        weaponRenderer.enabled = false;
    }
}
