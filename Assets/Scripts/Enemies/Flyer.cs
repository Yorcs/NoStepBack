using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Flyer : Enemy {
    private Weapon weapon;

    protected new void Start() {
        base.Start();
        weapon = GetComponentInChildren<Weapon>();
        Assert.IsNotNull(weapon);
    }

    protected new void FixedUpdate() {

        Vector3 target = manager.FindClosestPlayer(transform.position);

        direction = new Vector2 (target.x - transform.position.x, 0).normalized;

        base.FixedUpdate();

        if(IsActive())
            weapon.Fire(direction, TargetType.PLAYER, gameObject.layer);
    }

    //TODO: Swoop melee attack?
}
