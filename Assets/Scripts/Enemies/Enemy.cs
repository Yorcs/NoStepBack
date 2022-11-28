using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour, IEnemy {
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] protected float moveSpeed = 2f;
    protected Vector2 direction;

    private bool active = false;
    private EnemyHeart heart;

    //TODO: remove this
    [SerializeField] private bool IsBoss = false;

    Rigidbody2D enemyRB;
    protected EnemyManager manager;
    private int damage = 55;

    private bool isPoisoned, isFrozen;

    private float poisonDuration, freezeDuration;
    private float timeSinceLastPoison;
    private int statusDamage;



    // Start is called before the first frame update
    protected void Start() {
        currentHealth = maxHealth;
        direction = Vector2.left;

        enemyRB = GetComponent<Rigidbody2D>();
        manager = EnemyManager.instance;
        Assert.IsNotNull(enemyRB);
        Assert.IsNotNull(manager);
    }

    // Update is called once per frame
    protected void FixedUpdate() {
        if(active) {
            //todo: pick player to follow
            if(!isFrozen) {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                MoveHeart();
            }
            else {
                transform.Translate(direction * moveSpeed/2 * Time.deltaTime);
                MoveHeart();
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
        CreateHeart();
        currentHealth -= damage;
        UpdateHealthUI();
        if (IsDead()) {
            //todo: Drops/Pickups
            manager.LootDrop(transform.position);
            manager.MoneyDrop(transform.position);
            //TODO: remove this
            if(IsBoss) {
                manager.BossDefeated();
            }
            Destroy(heart.gameObject);
            manager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    private bool IsDead() {
        if (currentHealth <= 0) {
            return true;
        }

        return false;
    }

    private void CreateHeart() {
        if(heart == null) {
            heart = manager.GetUI(this);
            heart.SetPosition(transform.TransformPoint(transform.position));
            UpdateHealthUI();
        }
    }

    private void MoveHeart() {
        if(heart != null) {
            heart.SetPosition(transform.position);
        }
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
    
    public Vector3 GetPosition() {
        return transform.position;
    }

    public bool IsActive() {
        return active;
    }

    private void OnBecameVisible() {
        manager.AddEnemyToActive(this);
        if(!active) {
            active = true;
        }
    }
    
    private void OnBecameInvisible() {
        manager.RemoveEnemyFromActive(this);
    }

}
