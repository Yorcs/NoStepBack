using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour {

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
