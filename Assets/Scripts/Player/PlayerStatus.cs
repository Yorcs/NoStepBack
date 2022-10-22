using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerStatus : MonoBehaviour {

    [SerializeField] private int maxHitpoints = 3;
    private int currentHitPoints;

    private int pushBackDamage = 5;


    // Start is called before the first frame update
    void Start() {
        currentHitPoints = maxHitpoints;
    }

    public bool IsDead() {
        return currentHitPoints <= 0;
    }

    public void Revive() {
        if(!IsDead()) return;
        currentHitPoints = maxHitpoints;
    }

    public void PushBackEnemy(IEnemy enemy) {
        enemy.PushBack(pushBackDamage);
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;

        if (IsDead()) {
            Debug.Log("Dead!");
            //animation
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Reviver")) {
            PlayerReviver reviver = collision.gameObject.GetComponent<PlayerReviver>();
            Assert.IsNotNull(reviver);
            reviver.RevivePlayer();
        }
    }
}
