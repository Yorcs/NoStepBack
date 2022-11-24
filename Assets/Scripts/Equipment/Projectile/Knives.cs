using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Knives : AbstractProjectile {

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
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
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerStatus playerHit = other.gameObject.GetComponent<PlayerStatus>();
            Assert.IsNotNull(playerHit);

            playerHit.TakeDamage(damage);
            Destroy(gameObject);
        }
        if(other.gameObject.tag.Equals("Ground")) {
            //animation? Particle system?
            Destroy(gameObject);
        }
    }
}

