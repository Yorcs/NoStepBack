using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Grenade : AbstractProjectile {
    private Rigidbody2D grenadeRB;
    [SerializeField] Collider2D explosion;

    private int damage;
    private int fuse;
    private int fuseTimer;
    private float radius;

    List<IEnemy> enemiesInRange = new List<IEnemy>();

    private void Awake() {
        grenadeRB = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(grenadeRB);
        Assert.IsNotNull(explosion);
    }

    private void FixedUpdate() {
        fuseTimer++;
        if (fuseTimer > fuse) {
            Explode();
        }

    }

    private void Explode() {
        for(int i = enemiesInRange.Count - 1; i >= 0; i--) {
            IEnemy enemy = enemiesInRange[i];
        // foreach (IEnemy enemy in enemiesInRange) {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetFuse(int fuse) {
        this.fuse = fuse;
    }

    public void SetVelocity(Vector2 velocity) {
        grenadeRB.AddForce(velocity, ForceMode2D.Impulse);
    }

    public void SetRadius(float radius) {
        this.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Enemy")) {
            IEnemy enemy = other.GetComponent<IEnemy>();
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Enemy")) {
            IEnemy enemy = other.GetComponent<IEnemy>();
            if (enemiesInRange.Contains(enemy)) {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
