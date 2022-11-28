using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Ranger : Enemy {
    private Weapon weapon;

    protected new void Start() {
        base.Start();
        weapon = GetComponentInChildren<Weapon>();
        Assert.IsNotNull(weapon);
    }

    protected new void FixedUpdate() {
        base.FixedUpdate();
        if(IsActive())
            weapon.Fire(direction, TargetType.PLAYER, gameObject.layer);
    }

}
