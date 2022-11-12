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
    
    public override void UseSubweapon(Vector2 direction, int layer) {
        if(weaponReturned) {
            SpawnHalberd(direction, layer);
        }
    }

    private CrossHalberd SpawnHalberd(Vector2 direction, int layer) {
        GameObject GO = Instantiate(crossHalberdPrefab);
        GO.transform.position = transform.position;
        GO.layer = layer;

        CrossHalberd newHalberd = GO.GetComponent<CrossHalberd>();
        Assert.IsNotNull(newHalberd);

        newHalberd.SetDamage(damage);
        newHalberd.SetSpeed(speed);
        newHalberd.SetDirection(direction);
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
