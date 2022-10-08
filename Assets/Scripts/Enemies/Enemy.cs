using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour, IEnemy {
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private float moveSpeed = 2f;

    Rigidbody2D enemyRB;

    private int damage = 1;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;

        enemyRB = GetComponent<Rigidbody2D>();

        Assert.IsNotNull(enemyRB);
    }

    // Update is called once per frame
    void Update() {
        //todo: pick player to follow
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (IsDead()) {
            //todo: Drops/Pickups
            Destroy(gameObject);
        }
    }

    private bool IsDead() {
        if (currentHealth <= 0) {
            return true;
        }

        return false;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            Assert.IsNotNull(player);

            player.TakeDamage(damage);

            player.PushBackEnemy(this);
        }
    }

    public void PushBack(int damage) {
        enemyRB.velocity = Vector2.right * 10;
    }
}
