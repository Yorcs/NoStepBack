using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractWeapon : MonoBehaviour {
    [SerializeField] private GameObject bullet;

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

        Bullet newBullet = GO.GetComponent<Bullet>();

        Assert.IsNotNull(newBullet);

        newBullet.setDirection(Vector2.right);
        newBullet.setSpeed(bulletSpeed);
        newBullet.setDamage(damage);

        return newBullet;
    }
}
