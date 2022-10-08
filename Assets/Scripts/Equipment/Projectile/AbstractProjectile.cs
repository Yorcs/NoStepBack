using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour {

    void OnBecameInvisible() {
        Debug.Log("offScreen");
        Destroy(gameObject);
    }

}
