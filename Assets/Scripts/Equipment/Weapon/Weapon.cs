using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon : MonoBehaviour, IEquipment {
    [SerializeField] private GameObject bullet;
    private EquipmentType equipType = EquipmentType.WEAPON;
    private SpriteRenderer weaponRenderer;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] protected float spread;
    [SerializeField] protected int numBullets = 1;

    [SerializeField] protected float fireRate;

    [SerializeField] protected int criticalChance;
    private float fireTimer = 0;

    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int damage, penetration;
    //Status things to factor out later
    [SerializeField] protected bool doesFreeze, doesPoison = false;
    [SerializeField] protected float statusDuration = 0;
    [SerializeField] protected int statusDamage = 0;

    private EnemyManager enemyManager;

    private void Awake() {
        weaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(weaponRenderer);
        Assert.IsNotNull(bulletSpawnPoint);
    }

    public void Start() {
        enemyManager = EnemyManager.instance;
        Assert.IsNotNull(enemyManager);
    }

    public void Fire(Vector2 direction, int layer) {
        fireTimer += Time.deltaTime;

        Vector3 target = enemyManager.FindClosestVisibleEnemy(bulletSpawnPoint.position, direction);
        transform.rotation = Quaternion.identity;

        if((target - transform.position).magnitude > Screen.width) return;
        
        transform.rotation = LookAt2D(target - transform.position);
        if(direction.x < 0) transform.Rotate(0, 0, 180);

        if (fireTimer >= fireRate) {
            
            fireTimer = 0;
            for(int i = 0; i < numBullets; i++){
                SpawnBullet(target, layer);
            }
        }

    }

    private void SpawnBullet(Vector3 target, int layer) {
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

    public Sprite GetWeaponImage() {
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
}
