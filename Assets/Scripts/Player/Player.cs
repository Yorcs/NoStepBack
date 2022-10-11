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

    private List<Pickup> currentPickups = new List<Pickup>();

    // Start is called before the first frame update
    void Start() {
        currentHitPoints = maxHitpoints;

        playerRB = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<AbstractWeapon>();
        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(weapon);
        Assert.IsNotNull(currentPickups);
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
        PickupItem();
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

    private void PickupItem() {
        Debug.Log("Crouch Worked!");
        if (currentPickups.Count > 0) {
            Pickup closestPickup = currentPickups[0];
            float shortestDist = Vector2.Distance(transform.position, currentPickups[0].transform.position);

            foreach (Pickup pickup in currentPickups) {
                float currentDist = Vector2.Distance(transform.position, pickup.transform.position);
                if (currentDist < shortestDist) {
                    closestPickup = pickup;
                    shortestDist = currentDist;
                }
            }

            IEquipment newItem = closestPickup.GetItem();
            AbstractWeapon newWeapon = (AbstractWeapon)newItem;

            //Todo: consistent offset for weapons
            if (newWeapon.GetType() != weapon.GetType()) {
                //Todo: Drop current as pickup
                Destroy(weapon.gameObject);

                newWeapon.gameObject.transform.SetParent(transform);
                newWeapon.gameObject.transform.position = transform.position + new Vector3(1, 0, 0);
                weapon = newWeapon;

                currentPickups.Remove(closestPickup);
                Destroy(closestPickup.gameObject);
            } 
            // else {
            //    newItem.transform.SetParent(transform);
            //    newItem.transform.position = transform.position + new Vector3(1, 0, 0);
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag.Equals("Pickup")) {
            Pickup foundPickup = collision.gameObject.GetComponent<Pickup>();
            currentPickups.Add(foundPickup);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Pickup")) {
            Pickup foundPickup = collision.gameObject.GetComponent<Pickup>();
            if (currentPickups.Contains(foundPickup)) {
                currentPickups.Remove(foundPickup);
            }
        }
    }
}
