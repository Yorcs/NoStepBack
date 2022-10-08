using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : AbstractProjectile {
    private Vector2 direction = new Vector2(1,0);
    private float bulletSpeed = 1f;
    private int damage = 1;

    

    public void setDirection(Vector2 direction) {
        this.direction = direction;
    }

    public void setSpeed(float bulletSpeed) {
        this.bulletSpeed = bulletSpeed;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = collision.gameObject.GetComponent<IEnemy>();
            Assert.IsNotNull(enemyHit);

            enemyHit.TakeDamage(damage);

            //Todo: piercing?
            Destroy(gameObject);
        }
    }
}
