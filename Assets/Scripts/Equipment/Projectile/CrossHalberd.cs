using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CrossHalberd : AbstractProjectile {

    private CrossHalberdSubweapon returnTarget;
    private float returnTimer;
    private int penetration;

    private int damage;
    private float speed;
    private Vector2 direction;

    // Update is called once per frame
    void Update() {
        if(returnTimer >= 0 && penetration > 0) {
            transform.Translate(direction * speed * Time.deltaTime);
            returnTimer -= Time.deltaTime;
        } 
        else {
            //Todo: better return - returns to owner but if it hits ground, stops and drops as pickup.
            //Todo: Dropping this weapon while it's out causes errors
            transform.Translate(-direction * speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, returnTarget.gameObject.transform.position) < 1) {
                returnTarget.ReturnHalberd();
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void SetDirection(Vector2 direction) {
        this.direction = direction;
    }

    public void SetReturnTarget(CrossHalberdSubweapon returnTarget) {
        this.returnTarget = returnTarget;
    }

    public void SetReturnTimer(float returnTimer) {
        this.returnTimer = returnTimer;
    }

    public void SetPenetration(int penetration) {
        this.penetration = penetration;
    }

    void OnBecameInvisible() {
        if(returnTimer >= 0 && penetration > 0) {
            penetration = 0;
        } 
        else {
            returnTarget.ReturnHalberd();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Enemy")) {
            IEnemy enemyHit = other.gameObject.GetComponent<IEnemy>();
            Rigidbody2D rbEnemy = other.gameObject.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(enemyHit);

            enemyHit.TakeDamage(damage);
            penetration--;
        }
    }
}
