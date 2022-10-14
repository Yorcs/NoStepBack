using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {
    private Rigidbody2D playerRB;
    private AbstractWeapon weapon;
    private AbstractSubweapon subweapon;

    private Camera mainCam;

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
        subweapon = GetComponentInChildren<AbstractSubweapon>();
        mainCam = Camera.main;

        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(weapon);
        Assert.IsNotNull(currentPickups);

        //Are Starting subweapons a thing???
        //Assert.IsNotNull(subweapon);
    }

    // Update is called once per frame
    void FixedUpdate() {
        weapon.Fire(Vector2.right);
    }

    public void WalkHorizontal(int direction) {
        Vector2 position = transform.position;
        position.x += walkSpeed * Time.deltaTime * direction;
        float leftOfScreen = mainCam.ScreenToWorldPoint(Vector3.zero).x;
        float rightOfScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;

        position.x = Mathf.Clamp(position.x, leftOfScreen, rightOfScreen);
        Debug.Log(position);
        transform.position = position;
    }

    public void Jump() {

        playerRB.AddForce(Vector2.up * jumpStrength);
    }

    public void Crouch() {
        PickupItem();
    }

    public void Special() {
        if (subweapon != null) {
            subweapon.UseSubweapon();
        }
    }

    public void Backwards()
    {
        Vector2 position = transform.position;
        position.x -= walkSpeed * Time.deltaTime;
        transform.position = position;
    }

    public void TakeDamage(int damage) {
        currentHitPoints -= damage;

        if (IsDead()) {
            Debug.Log("Dead!");
        }
    }

    public void PushBackEnemy(IEnemy enemy) {
        Debug.Log(enemy);
        enemy.PushBack(pushBackDamage);
    }

    private bool IsDead() {
        if (currentHitPoints <= 0) {
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

            bool itemTaken = false;
            IEquipment newItem = closestPickup.GetItem();
            switch (newItem.GetEquipmentType()) {
                case equipmentType.WEAPON:
                    AbstractWeapon newWeapon = (AbstractWeapon)newItem;
                    itemTaken = pickupWeapon(newWeapon);
                    break;
                case equipmentType.SUBWEAPON:
                    AbstractSubweapon newSubweapon = (AbstractSubweapon)newItem;
                    itemTaken = pickupSubweapon(newSubweapon);
                    break;
                case equipmentType.MOD:
                    //take Mod
                    break;
                default:
                    Debug.Log("Invalid Item");
                    break;
            }

            if (itemTaken) {
                currentPickups.Remove(closestPickup);
                Destroy(closestPickup.gameObject);
            }

        }
    }

    //Todo: consistent offset for weapons
    private bool pickupWeapon(AbstractWeapon newWeapon) {
        if (newWeapon.GetType() != weapon.GetType()) {
            //Todo: Drop current as pickup
            Destroy(weapon.gameObject);

            //TOodo: fix size
            newWeapon.gameObject.transform.SetParent(transform);
            newWeapon.gameObject.transform.position = transform.position + new Vector3(1, 0, 0);
            weapon = newWeapon;
            return true;
        }
        return false;
    }

    private bool pickupSubweapon(AbstractSubweapon newSubweapon) {
        if (subweapon == null || newSubweapon.GetType() != subweapon.GetType()) {
            //Todo: Drop current as pickup
            if (subweapon != null) {
                Destroy(subweapon.gameObject);
            }
            //Todo: fix size
            newSubweapon.gameObject.transform.SetParent(transform);
            newSubweapon.gameObject.transform.position = transform.position;
            subweapon = newSubweapon;
            return true;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Pickup")) {
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
