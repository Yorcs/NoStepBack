using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random=UnityEngine.Random;

public class Bullet : AbstractProjectile {
    private WeaponStatus status;
    private Vector2 direction = Vector2.right;
    private float bulletSpeed = 1f;
    private int damage = 1;
    private int criticalChance = 0;
    private int criticalMultiplier = 2;
    private int penetration = 1;

    private void Start(){
        status = GetComponent<WeaponStatus>();
    }

    public void SetDirection(Vector2 direction) {
        this.direction = direction;
        //Todo: improve this to use actual rotation
        transform.localScale = new Vector2(direction.x * Mathf.Abs(transform.localScale.x),  transform.localScale.y);
    }
    
    public void SetCritical(int criticalChance)
    {
        this.criticalChance = criticalChance;
    }

    public void SetCriticalMultiplier(int criticalMultiplier) {
        this.criticalMultiplier = criticalMultiplier;
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

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Rigidbody2D rbEnemy = other.gameObject.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(enemyHit);

            int randomVal = Random.Range(1,100);
            int totalDamage = randomVal < criticalChance? 
                    (damage * criticalMultiplier) : damage;
            enemyHit.TakeDamage(totalDamage); 
            

            if(status.GetPoisoned()) {
                enemyHit.SetPoison(status.GetStatusDuration(), status.GetStatusDamage());
            }
            if(status.GetFrozen()) {
                enemyHit.SetFreeze(status.GetStatusDuration());
            }

            penetration -= 1;
            if(penetration <= 0) {
               Destroy(gameObject);
            }

        }
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            Rigidbody2D rbPlayer = other.gameObject.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(player);

            player.TakeDamage(damage);

            Destroy(gameObject);
        }
        if(other.gameObject.tag.Equals("Ground")) {
            //animation? Particle system?
            Destroy(gameObject);
        }
        
    }
}
