using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractWeapon : MonoBehaviour, IEquipment {
    [SerializeField] private GameObject bullet;
    private equipmentType equipType = equipmentType.WEAPON;

    [SerializeField] protected float spread;

    [SerializeField] protected int fireRate;
    private int fireTimer = 0;

    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int damage;

    public void Fire(Vector2 target) {
        fireTimer++;
        if (fireTimer % fireRate == 0) {
            fireTimer = 0;

            Bullet newBullet = SpawnBullet();
        }

    }

    private Bullet SpawnBullet() {
        GameObject GO = Instantiate(bullet);
        GO.transform.position = transform.position;
        GO.transform.rotation = transform.rotation;

        //randomizing spread
        var xSpread = Random.Range(0f, spread) * 90;
        var ySpread = Random.Range(0f, spread) * 90;
        var spreadvec = new Vector3(xSpread, ySpread, 0);

        //adding the spread

        GO.transform.Rotate(spreadvec);

        Bullet newBullet = GO.GetComponent<Bullet>();

        Assert.IsNotNull(newBullet);

        newBullet.SetDirection(Vector2.right);
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetDamage(damage);

        return newBullet;
    }

    public equipmentType GetEquipmentType() {
        return equipType;
    }
}
