using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : AbstractProjectile {
    private Vector2 direction = new Vector2(1, 0);
    private float bulletSpeed = 1f;
    private int damage = 1;
    private int bulletTime = 1; //for poisoning purposes
    private int stoppingTime = 1;
    private int penetration = 1;

    public void SetBulletTime(int bulletTime)
    {
        this.bulletTime = bulletTime;
    }

    public void SetStoppingTime(int stoppingTime)
    {
        this.stoppingTime = stoppingTime;
    }

    public void SetDirection(Vector2 direction) {
        this.direction = direction;
    }

    public void SetSpeed(float bulletSpeed) {
        this.bulletSpeed = bulletSpeed;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetPenetration(int penetration) {
        this.penetration = penetration;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Rigidbody2D rbEnemy = other.gameObject.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(enemyHit);

            //poisoning
            for (int i = 0; i < bulletTime; i++)
            {
                enemyHit.TakeDamage(damage);
            }

            //stopping power
            for (int i = 0; i < stoppingTime; i++)
            {
                rbEnemy.velocity = Vector3.zero;
            }

            //Todo: piercing?
            penetration -= 1;
            if(penetration <= 0) {
               Destroy(gameObject);
            }
        }
    }

    
}
