using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private Rigidbody2D moneyRB;

    public void SetMoney(int money)
    {
        this.money = money;
    }

    public void SetForce(Vector2 force)
    {
       moneyRB.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            player.GainMoney(money);
            
            //Play pickup sound
            FindObjectOfType<AudioManager>().Play("CoinPickup1");
            
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        //TODO: this should maybe sheck which edge of the screen its gone off
        Destroy(this.gameObject);
    }
}
