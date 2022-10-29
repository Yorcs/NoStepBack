using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int money;

    public void SetMoney(int money)
    {
        this.money = money;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            player.GainMoney(money);
            Destroy(gameObject);
        }
    }
}
