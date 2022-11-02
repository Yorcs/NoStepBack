using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour, IEnemy {
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 direction;

    private bool active = false;
    private EnemyHeart heart;

    Rigidbody2D enemyRB;
    EnemyManager manager;

    private Weapon weapon;
    private int damage = 55;

    private bool isPoisoned, isFrozen;

    private float poisonDuration, freezeDuration;
    private float timeSinceLastPoison;
    private int statusDamage;



    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        direction = Vector2.left;

        enemyRB = GetComponent<Rigidbody2D>();
        manager = GetComponentInParent<EnemyManager>();
        weapon = GetComponentInChildren<Weapon>();
        //bullet = GetComponent<Bullet>();
        Assert.IsNotNull(enemyRB);
        Assert.IsNotNull(manager);
        
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(active) {
            //todo: clean this up maybe?
            if(weapon) weapon.Fire(direction);
            //todo: pick player to follow
            if(!isFrozen) {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                heart.SetPosition(transform.position);
            }
            else {
                transform.Translate(direction * moveSpeed/2 * Time.deltaTime);
                heart.SetPosition(transform.position);
                DoFreezeTick();
            }
            //poisoning
            if(isPoisoned)
            {
                DoPoisonTick();
            }
        }
    }

    private void DoPoisonTick() {
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
        UpdateHealthUI();
        if (IsDead()) {
            //todo: Drops/Pickups
            manager.LootDrop(transform.position);
            manager.MoneyDrop(transform.position);
            Destroy(heart.gameObject);
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
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            Assert.IsNotNull(player);

            player.TakeDamage(damage);

            player.PushBackEnemy(this);
        }
        if(collision.gameObject.tag.Equals("Walls"))
        {
            direction *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void UpdateHealthUI() {
        float healthPercent = (float) currentHealth / (float) maxHealth;
        heart.SetHealthPercent(healthPercent);
    }
    
    private void OnBecameVisible() {
        if(!active) {
            active = true;
            heart = manager.GetUI(this);
            heart.SetPosition(transform.TransformPoint(transform.position));
            UpdateHealthUI();
        }
    }
    
    // private void OnBecameInvisible() {
    //     active = false;
    //     if (!IsDead())
    //     {
    //         Destroy(heart.gameObject);
    //     }
    // }

}
