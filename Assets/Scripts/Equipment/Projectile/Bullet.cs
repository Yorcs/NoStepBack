using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : AbstractProjectile {
    private Vector2 direction = new Vector2(1, 0);
    private float bulletSpeed = 1f;
    private int damage = 1;
    private int bulletTime = 1; //for poisoning purposes

    public void SetBulletTime(int bulletTime)
    {
        this.bulletTime = bulletTime;
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

    // Update is called once per frame
    void Update() {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Assert.IsNotNull(enemyHit);

            for (int i = 0; i < bulletTime; i++)
            {
                enemyHit.TakeDamage(damage);
            }

            //Todo: piercing?
            //Todo: Stopping power?
            Destroy(gameObject);
        }
    }
}
