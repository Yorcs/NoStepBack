using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CrossHalberdSubweapon : AbstractSubweapon {
    [SerializeField] private GameObject crossHalberdPrefab;

    [SerializeField] private float speed;
    [SerializeField] private int penetration;
    [SerializeField] private float returnTimer;

    private bool weaponReturned = true;


    // Start is called before the first frame update
    void Start() {
        subweaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
        Assert.IsNotNull(crossHalberdPrefab);
    }
    
    public override void UseSubweapon() {
        if(weaponReturned) {
            SpawnHalberd();
        }
    }

    private CrossHalberd SpawnHalberd() {
        GameObject GO = Instantiate(crossHalberdPrefab);
        GO.transform.position = transform.position;

        CrossHalberd newHalberd = GO.GetComponent<CrossHalberd>();
        Assert.IsNotNull(newHalberd);

        newHalberd.SetDamage(damage);
        newHalberd.SetSpeed(speed);
        newHalberd.SetDirection(Vector2.right);
        newHalberd.SetPenetration(penetration);
        newHalberd.SetReturnTimer(returnTimer);
        newHalberd.SetReturnTarget(this);

        weaponReturned = false;
        return newHalberd;
    }

    public void ReturnHalberd() {
        weaponReturned = true;
    }
}
