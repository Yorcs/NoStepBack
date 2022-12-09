using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random=UnityEngine.Random;

public class Blast : AbstractProjectile {

    private Vector3 scaleChange;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        scaleChange = new Vector3(0.001f, 0.001f, 0.001f);
        transform.localScale += scaleChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Assert.IsNotNull(enemyHit);

            int randomVal = Random.Range(1, 100);
            int totalDamage = randomVal < criticalChance ?
                    (damage * criticalMultiplier) : damage;
            enemyHit.TakeDamage(totalDamage);


            if (doesPoison)
            {
                enemyHit.SetPoison(statusDuration, statusDamage);
            }
            if (doesFreeze)
            {
                enemyHit.SetFreeze(statusDuration);
            }

            penetration -= 1;
            if (penetration <= 0)
            {
                Destroy(gameObject);
            }

        }
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            Assert.IsNotNull(player);

            if (!player.IsDead())
            {
                player.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag.Equals("Ground") || other.gameObject.tag.Equals("Wall"))
        {
            //animation? Particle system?
            Destroy(gameObject);
        }

    }
}
