using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int money;

    public void setMoney(int money)
    {
        this.money = money;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            player.GainMoney(money);
            Destroy(gameObject);
        }
    }
}
