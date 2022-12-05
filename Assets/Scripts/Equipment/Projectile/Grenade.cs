using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Grenade : AbstractProjectile {
    private Rigidbody2D grenadeRB;
    [SerializeField] CircleCollider2D explosion;
    private float fuse;
    private float fuseTimer;
    [SerializeField] private GameObject explosionPrefab;

    List<IEnemy> enemiesInRange = new List<IEnemy>();

    List<PlayerStatus> playersInRange = new();

    private void Awake() {
        grenadeRB = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(grenadeRB);
        Assert.IsNotNull(explosion);
        Assert.IsNotNull(explosionPrefab);
    }

    private void FixedUpdate() {
        fuseTimer += Time.deltaTime;
        Debug.Log(playersInRange.Count);
        if (fuseTimer > fuse) {
            Explode();
        }
    }

    private void Explode() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //enemies iterated backwards for deletion's sakes. Players are never deleted
        for(int i = enemiesInRange.Count - 1; i >= 0; i--) {
            IEnemy enemy = enemiesInRange[i];
            enemy.TakeDamage(damage);
        }
        
        foreach(PlayerStatus player in playersInRange) {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetFuse(float fuse) {
        this.fuse = fuse;
    }

    public void SetVelocity(Vector2 velocity) {
        grenadeRB.AddForce(velocity, ForceMode2D.Impulse);
    }

    public void SetRadius(float radius) {
        explosion.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Enemy")) {
            IEnemy enemy = other.GetComponent<IEnemy>();
            enemiesInRange.Add(enemy);
        }
        if(other.tag.Equals("Player")) {
            PlayerStatus player = other.GetComponent<PlayerStatus>();
            playersInRange.Add(player);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Enemy")) {
            IEnemy enemy = other.GetComponent<IEnemy>();
            if (enemiesInRange.Contains(enemy)) {
                enemiesInRange.Remove(enemy);
            }
        }
        if (other.tag.Equals("Player")) {
            PlayerStatus player = other.GetComponent<PlayerStatus>();
            if (playersInRange.Contains(player)) {
                playersInRange.Remove(player);
            }
        }
    }
}
