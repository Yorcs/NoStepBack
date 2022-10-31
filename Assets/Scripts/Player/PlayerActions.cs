using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerActions : MonoBehaviour {
    private PlayerStatus status;

    private Weapon weapon;
    private AbstractSubweapon subweapon;

    private PlayerWeaponUI weaponUI;

    private List<Pickup> currentPickups = new List<Pickup>();

    // Start is called before the first frame update
    void Start() {
        status = GetComponent<PlayerStatus>();
        weapon = GetComponentInChildren<Weapon>();
        subweapon = GetComponentInChildren<AbstractSubweapon>();

        Assert.IsNotNull(weapon);
        Assert.IsNotNull(status);
    }

    // Update is called once per frame
    void Update() {
        if(currentPickups.Count > 0) {
            Pickup closestPickup = GetClosestPickup();
            weaponUI.ShowPopup(closestPickup.GetEquipmentType(), closestPickup.GetDamage(), closestPickup.GetFireRate());
        } else {
            weaponUI.HidePopup();
        }
        if(status.IsDead()) return;
        weapon.Fire(Vector2.right);
    }


    public void OnSpecial(InputAction.CallbackContext context) {
        if(status.IsDead() || context.started || context.canceled) return;
        if (subweapon != null) {
            subweapon.UseSubweapon();
        }
    }

    public void OnPickup(InputAction.CallbackContext context) {
        if(status.IsDead() || context.started || context.canceled) return;
        PickupItem();
    }

    private void PickupItem() {
        if (currentPickups.Count > 0) {
            Pickup closestPickup = GetClosestPickup();

            bool itemTaken = false;
            IEquipment newItem = closestPickup.GetItem();
            switch (newItem.GetEquipmentType()) {
                case EquipmentType.WEAPON:
                    Weapon newWeapon = (Weapon)newItem;
                    itemTaken = pickupWeapon(newWeapon);
                    break;
                case EquipmentType.SUBWEAPON:
                    AbstractSubweapon newSubweapon = (AbstractSubweapon)newItem;
                    itemTaken = pickupSubweapon(newSubweapon);
                    break;
                case EquipmentType.MOD:
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

    private Pickup GetClosestPickup() {
        Pickup closestPickup = currentPickups[0];
        float shortestDist = Vector2.Distance(transform.position, currentPickups[0].transform.position);

        foreach (Pickup pickup in currentPickups) {
            float currentDist = Vector2.Distance(transform.position, pickup.transform.position);
            if (currentDist < shortestDist) {
                closestPickup = pickup;
                shortestDist = currentDist;
            }
        }
        return closestPickup;
    }

    //Todo: consistent offset for weapons
    private bool pickupWeapon(Weapon newWeapon) {
        //Todo: Drop current as pickup
        Destroy(weapon.gameObject);

        //Todo: fix size
        //Todo: Fix position offset
        newWeapon.gameObject.transform.SetParent(transform);
        newWeapon.gameObject.transform.position = transform.position + new Vector3(2, 0, 0);
        weapon = newWeapon;
        weaponUI.SetWeapon(weapon.GetWeaponImage());
        return true;
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
            weaponUI.SetSubweapon(subweapon.GetSubweaponImage());
            return true;
        }
        return false;
    }

    public void SetUI(PlayerWeaponUI weaponUI) {
        this.weaponUI = weaponUI;
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
