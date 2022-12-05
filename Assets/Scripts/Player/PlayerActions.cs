using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerActions : MonoBehaviour {
    private PlayerStatus status;
    private PlayerController controller;
    [SerializeField] private Transform weaponAttachPoint;

    private Weapon weapon;
    private AbstractSubweapon subweapon;

    private PlayerWeaponUI weaponUI;

    private List<Pickup> currentPickups = new List<Pickup>();

    //private bool autoFireActive = true;

    // Start is called before the first frame update
    void Start() {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
        weapon = GetComponentInChildren<Weapon>();
        subweapon = GetComponentInChildren<AbstractSubweapon>();

        Assert.IsNotNull(weapon);
        Assert.IsNotNull(status);
        Assert.IsNotNull(controller);
    }

    // Update is called once per frame
    void Update() {
        if(currentPickups.Count > 0) {
            Pickup closestPickup = GetClosestPickup();
            weaponUI.ShowPopup(closestPickup.GetEquipmentType(), closestPickup.GetStats());
        } else {
            weaponUI.HidePopup();
        }
        if(status.IsDead()) return;

        TargetType target = TargetType.ENEMY;

        if(status.IsInPVP()) target = TargetType.PVP;
        
        weapon.Fire(controller.GetDirection(), target, gameObject.layer);
    }

    // public void OnFire(InputAction.CallbackContext context) {
    //     if (status.IsDead() || autoFireActive || context.started || context.canceled) return;
    //     weapon.Fire(gameObject.layer);
    // }

    public void OnSpecial(InputAction.CallbackContext context) {
        if(status.IsDead() || context.started || context.canceled) return;
        if (subweapon != null) {
            subweapon.UseSubweapon(controller.GetDirection(), gameObject.layer);
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
                    itemTaken = PickupWeapon(newWeapon);
                    break;
                case EquipmentType.SUBWEAPON:
                    AbstractSubweapon newSubweapon = (AbstractSubweapon)newItem;
                    itemTaken = PickupSubweapon(newSubweapon);
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
    private bool PickupWeapon(Weapon newWeapon) {
        //Todo: Drop as pickup
        PickupFactory.instance.CreatePickupFromEquipment(transform.position, weapon, weapon.transform);

        Vector2 direction = controller.GetDirection();
        newWeapon.gameObject.transform.localScale = new Vector2(direction.x * Mathf.Abs(newWeapon.gameObject.transform.localScale.x), newWeapon.gameObject.transform.localScale.y);
        newWeapon.gameObject.transform.SetParent(transform);
        newWeapon.gameObject.transform.position = weaponAttachPoint.position;
        weapon = newWeapon;
        weaponUI.SetWeapon(weapon.GetEquipmentImage());
        return true;
    }

    private bool PickupSubweapon(AbstractSubweapon newSubweapon) {
        //Todo: Drop current as pickup
        if (subweapon != null) {
            PickupFactory.instance.CreatePickupFromEquipment(transform.position, subweapon, subweapon.transform);
        }
        //Todo: fix size
        newSubweapon.gameObject.transform.SetParent(transform);
        newSubweapon.gameObject.transform.position = transform.position;
        subweapon = newSubweapon;
        weaponUI.SetSubweapon(subweapon.GetEquipmentImage());
        return true;
    }

    public void SetUI(PlayerWeaponUI weaponUI) {
        this.weaponUI = weaponUI;
    }

    public void SetWeapon(GameObject weaponPrefab) {
        GameObject GO = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        Weapon newWeapon = GO.GetComponent<Weapon>();
        Assert.IsNotNull(newWeapon);

        Destroy(weapon.gameObject);

        Vector2 direction = controller.GetDirection();
        Debug.Log(direction);
        newWeapon.gameObject.transform.localScale = new Vector2(direction.x * Mathf.Abs(newWeapon.gameObject.transform.localScale.x), newWeapon.gameObject.transform.localScale.y);
        newWeapon.gameObject.transform.SetParent(transform);
        newWeapon.gameObject.transform.position = weaponAttachPoint.position;
        weapon = newWeapon;
        weaponUI.SetWeapon(weapon.GetEquipmentImage());
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
