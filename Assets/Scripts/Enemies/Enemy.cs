using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour, IEnemy {
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private float moveSpeed = 2f;

    private bool active = false;

    Rigidbody2D enemyRB;
    EnemyManager manager;

    private int damage = 1;

    private bool isPoisoned, isFrozen;

    private float poisonDuration, freezeDuration;
    private float timeSinceLastPoison;
    private int statusDamage;



    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;

        enemyRB = GetComponent<Rigidbody2D>();
        manager = GetComponentInParent<EnemyManager>();
        //bullet = GetComponent<Bullet>();
        Assert.IsNotNull(enemyRB);
        Assert.IsNotNull(manager);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(active) {
            //todo: pick player to follow
            if(!isFrozen) {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else {
                transform.Translate(Vector2.left * moveSpeed/2 * Time.deltaTime);
                DoFreezeTick();
            }
            //poisoning
            if(isPoisoned)
            {
                doPoisonTick();
            }
        }
    }

    private void doPoisonTick() {
        poisonDuration -= Time.deltaTime;
        if(poisonDuration <= 0) {
            isPoisoned = false;
            timeSinceLastPoison = 0;
        }
        timeSinceLastPoison += Time.deltaTime;
        if(timeSinceLastPoison >= 1) {
            TakeDamage(statusDamage);
            timeSinceLastPoison = 0;
        }
    }

    private void DoFreezeTick() {
        freezeDuration -= Time.deltaTime;
        if(freezeDuration <= 0) {
            isFrozen = false;
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (IsDead()) {
            //todo: Drops/Pickups
            manager.LootDrop(transform.position);

            Destroy(gameObject);
        }
    }

    private bool IsDead() {
        if (currentHealth <= 0) {
            return true;
        }

        return false;
    }



    public void SetPoison(float duration, int damage)
    {
        isPoisoned = true;
        poisonDuration = duration;
        statusDamage = damage;
    }

    public void SetFreeze(float duration)
    {
        isFrozen = true;
        freezeDuration = duration;
    }

    public void PushBack(int damage) {
        enemyRB.velocity = Vector2.right * 10;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            Assert.IsNotNull(player);

            player.TakeDamage(damage);

            player.PushBackEnemy(this);
        }
    }
    
    private void OnBecameVisible() {
        active = true;
    }
    
    private void OnBecameInvisible() {
        active = false;
    }

}
