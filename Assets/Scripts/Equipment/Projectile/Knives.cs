using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Knives : AbstractProjectile
{
    private Vector2 direction = new Vector2(1, 0);
    private float knifeSpeed;
    private int damage;

    void Update() {
        transform.Translate(direction * knifeSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void SetSpeed(float knifeSpeed)
    {
        this.knifeSpeed = knifeSpeed;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Assert.IsNotNull(enemyHit);

            enemyHit.TakeDamage(damage);
            Destroy(gameObject);
        }
        if(other.gameObject.tag.Equals("Ground")) {
            //animation? Particle system?
            Destroy(gameObject);
        }
    }
}

