using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Flame : AbstractProjectile {
    private float lifetime;

    private void Start() {
        lifetime = Random.Range(0.3f, 0.7f);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if(lifetime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Assert.IsNotNull(enemyHit);

            int randomVal = Random.Range(1,100);
            int totalDamage = randomVal < criticalChance? 
                    (damage * criticalMultiplier) : damage;
            enemyHit.TakeDamage(totalDamage); 
            
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            Assert.IsNotNull(player);

            if(!player.IsDead()) {
                player.TakeDamage(damage);
            }
        }
        if(other.gameObject.tag.Equals("Ground") || other.gameObject.tag.Equals("Wall")) {
            //animation? Particle system?
            Destroy(gameObject);
        }
        
    }
}
