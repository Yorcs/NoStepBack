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

    private void Start() {
        weaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(weaponRenderer);
        Assert.IsNotNull(bulletSpawnPoint);
    }

    public void Fire(Vector2 target, int layer) {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate) {
            
            fireTimer -= fireRate;
            for(int i = 0; i < numBullets; i++){
                Bullet newBullet = SpawnBullet(target, layer);
            }
        }

    }

    private Bullet SpawnBullet(Vector2 target, int layer) {
        GameObject GO = Instantiate(bullet);
        GO.transform.position = bulletSpawnPoint.position;
        GO.transform.rotation = bulletSpawnPoint.rotation;
        
        GO.layer = layer;
        //randomizing spread
        var zSpread = Random.Range(-spread, spread);
        var spreadvec = new Vector3(0, 0, zSpread);

        //adding the spread

        GO.transform.Rotate(spreadvec);

        Bullet newBullet = GO.GetComponent<Bullet>();

        Assert.IsNotNull(newBullet);

        newBullet.SetDirection(target);
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetDamage(damage);
        newBullet.SetCritical(criticalChance);
        newBullet.SetPenetration(penetration);
        
        //Status Stuff to refactor later
        newBullet.SetIsPoisoned(doesPoison);
        newBullet.SetIsFrozen(doesFreeze);
        newBullet.SetStatusDuration(statusDuration);
        newBullet.SetStatusDamage(statusDamage);

        return newBullet;
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
}
