using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerStatus : MonoBehaviour {
    private PlayerController controller;

    [SerializeField] private int maxHitpoints = 300;
    private int currentHitPoints;

    private PlayerHealthUI healthUI;
    private int money;
    private PlayerMoneyUI moneyUI;

    private int pushBackDamage = 5;

    private float respawnDuration = 10f;
    private float respawnTimer;


    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<PlayerController>();
        Assert.IsNotNull(controller);
        currentHitPoints = maxHitpoints;
        money = 0;
    }

    private void Update() {
        if(IsDead()) {
            respawnTimer += Time.deltaTime;
            if(respawnTimer >= respawnDuration) {
                controller.Respawn();
                Revive();
            }
        }
    }

    public bool IsDead() {
        return currentHitPoints <= 0;
    }

    //Todo: Steal Money
    public void Revive() {
        if(!IsDead()) return;
        currentHitPoints = maxHitpoints;
        healthUI.SetHealth(currentHitPoints);
        respawnTimer = 0;
    }

    public void GainMoney(int money)
    {
        this.money += money;
        moneyUI.SetMoney(this.money);
    }

    public void GainHealth(int health) {
        currentHitPoints += health;
        currentHitPoints = Mathf.Clamp(currentHitPoints, 0, maxHitpoints);
        healthUI.SetHealth(currentHitPoints);
    }

    public void PushBackEnemy(IEnemy enemy) {
        enemy.PushBack(pushBackDamage);
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;
        healthUI.SetHealth(currentHitPoints);

        if (IsDead()) {
            Debug.Log("Dead!");
        }
    }

    public void SetUI(PlayerHealthUI healthUI, PlayerMoneyUI moneyUI) {
        this.healthUI = healthUI;
        this.moneyUI = moneyUI;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Reviver")) {
            PlayerReviver reviver = collision.gameObject.GetComponent<PlayerReviver>();
            Assert.IsNotNull(reviver);
            reviver.RevivePlayer();
        }
    }
}
