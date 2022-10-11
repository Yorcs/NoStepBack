using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : AbstractSubweapon {
    [SerializeField] GameObject MinePrefab;

    private void Start() {
        fireRate = 5;
        damage = 150;
    }

    public void UseSubweapon() {

    }

}
