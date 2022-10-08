using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {
    private Rigidbody2D playerRB;
    private AbstractWeapon weapon;

    [SerializeField] private int maxHitpoints = 3;
    private int currentHitPoints;

    public float walkSpeed = 7f;
    public float jumpStrength = 20f;

    private int pushBackDamage = 5;

    // Start is called before the first frame update
    void Start() {
        currentHitPoints = maxHitpoints;

        playerRB = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<AbstractWeapon>();
        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(weapon);
    }

    // Update is called once per frame
    void FixedUpdate() {
        weapon.Fire(Vector2.right);
    }

    public void Walk() {
        Vector2 position = transform.position;
        position.x += walkSpeed * Time.deltaTime;
        transform.position = position;
    }

    public void Jump() {

        playerRB.AddForce(Vector2.up * jumpStrength);
    }

    public void Crouch() {
        Debug.Log("Crouch NYI");
    }

    public void Special() {
        Debug.Log("Special NYI");
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;

        if(IsDead()) {
            Debug.Log("Dead!");
        }
    }

    public void PushBackEnemy(IEnemy enemy) {
        Debug.Log(enemy);
        enemy.PushBack(pushBackDamage);
    }

    private bool IsDead() {
        if(currentHitPoints <= 0) {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag.Equals("Pickup")) {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            Assert.IsNotNull(pickup);

            GameObject newItem = pickup.GetItem();

            //Todo: account for different types of equipment
            //Todo: Account for not picking up duplicates
            //Todo: consistent offset for weapon
            Destroy(weapon.gameObject);
            
            newItem.transform.SetParent(transform);
            newItem.transform.position = transform.position + new Vector3(1,0,0);
            weapon = newItem.GetComponent<AbstractWeapon>();

        }
    }
}
