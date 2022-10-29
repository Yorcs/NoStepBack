using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerStatus : MonoBehaviour {
    private PlayerController controller;

    [SerializeField] private int maxHitpoints = 3;
    private int currentHitPoints;

    private PlayerHealthUI healthUI;
    private int money;

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
                currentHitPoints = maxHitpoints;
                controller.Respawn();
                respawnTimer = 0;
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
        Debug.Log(money);
    }

    public void PushBackEnemy(IEnemy enemy) {
        enemy.PushBack(pushBackDamage);
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;
        healthUI.SetHealth(currentHitPoints);

        if (IsDead()) {
            Debug.Log("Dead!");
            //animation
        }
    }

    public void SetUI(PlayerHealthUI healthUI) {
        this.healthUI = healthUI;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Reviver")) {
            PlayerReviver reviver = collision.gameObject.GetComponent<PlayerReviver>();
            Assert.IsNotNull(reviver);
            reviver.RevivePlayer();
        }
    }
}
