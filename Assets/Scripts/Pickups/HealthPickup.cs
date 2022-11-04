using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
    [SerializeField] private int healthRestored;
    [SerializeField] private Rigidbody2D HealthRB;

    public void SetMoney(int healthRestored)
    {
        this.healthRestored = healthRestored;
    }

    public void SetForce(Vector2 force)
    {
       HealthRB.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            player.GainHealth(healthRestored);
            Destroy(gameObject);
        }
    }
}
