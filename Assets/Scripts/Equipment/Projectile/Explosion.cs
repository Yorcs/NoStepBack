using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float delay = 1f;

    // Update is called once per frame
    void Update() {
        delay -= Time.deltaTime;
        if(delay <= 0) Destroy(gameObject);
    }
}
