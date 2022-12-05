using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerStatus : MonoBehaviour {
    private PlayerController controller;

    [SerializeField] private int maxHitpoints = 300;
    private int currentHitPoints;

    private PlayerHealthUI healthUI;
    private PlayerRespawnUI respawnUI;
    private PlayerRespawner respawner;
    private int money;
    private PlayerMoneyUI moneyUI;

    private int pushBackDamage = 5;

    private float respawnDuration = 10f;
    private float respawnTimer;

    private bool inPVP;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        Assert.IsNotNull(controller);
        Assert.IsNotNull(animator);
        currentHitPoints = maxHitpoints;
        money = 0;
    }

    private void Update() {
        if(IsDead() && !inPVP) {
            
            respawnTimer += Time.deltaTime;
            respawner.SetRespawn(respawnTimer);
            respawner.SetPosition(transform.TransformPoint(transform.position));
            if (respawnTimer >= respawnDuration) {
                Destroy(respawner.gameObject);
                controller.Respawn();
                Revive();
            }
        }
    }

    public PlayerRespawner GetUI()
    {
        PlayerRespawner respawner = respawnUI.MakeRespawner(this);
        Debug.Log("respawner created");
        return respawner;
    }

    private void CreateRespawn()
    {
            respawner = GetUI();
            respawner.SetPosition(transform.TransformPoint(transform.position));
    }


    public bool IsDead() {
        return currentHitPoints <= 0;
    }

    public bool IsInPVP() {
        return inPVP;
    }

    //Todo: Steal Money
    public void Revive() {
        if(!IsDead() || inPVP) return;
        currentHitPoints = maxHitpoints;
        healthUI.SetHealth(currentHitPoints);
        respawnTimer = 0;
        animator.SetBool("IsDead", IsDead());
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

    public void FillHealth() {
        currentHitPoints = maxHitpoints;
        healthUI.SetHealth(currentHitPoints);
    }

    public void PushBackEnemy(IEnemy enemy) {
        enemy.PushBack(pushBackDamage);
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;
        healthUI.SetHealth(currentHitPoints);

        if (IsDead()) {
            CreateRespawn();
            Debug.Log("Dead!");
            animator.SetBool("IsDead", IsDead());
        }
    }

    public void SetUI(PlayerHealthUI healthUI, PlayerMoneyUI moneyUI) {
        this.healthUI = healthUI;
        this.moneyUI = moneyUI;
    }

    public void SetPVP(bool inPVP) {
        this.inPVP = inPVP;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Reviver")) {
            PlayerReviver reviver = collision.gameObject.GetComponent<PlayerReviver>();
            Assert.IsNotNull(reviver);
            reviver.RevivePlayer();
        }
    }
}
