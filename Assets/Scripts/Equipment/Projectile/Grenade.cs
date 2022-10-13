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
        //TODO: actually explode
        
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

    internal void SetRadius(float radius) {
        this.radius = radius;
    }
}
