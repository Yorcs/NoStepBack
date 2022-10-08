using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {
    private Rigidbody2D playerRB;

    private int hitpoints = 3;

    public float walkSpeed = 3f;
    public float jumpStrength = 20f;

    // Start is called before the first frame update
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRB);
    }

    // Update is called once per frame
    void Update() {

    }

    public void Walk() {
        Vector2 position = transform.position;
        position.x += walkSpeed * Time.deltaTime;
        transform.position = position;
    }

    public void Jump() {

        playerRB.AddForce(Vector2.up * jumpStrength);
    }

    public void Crouch() {
        Debug.Log("Crouch NYI");
    }

    public void Special() {
        Debug.Log("Special NYI");
    }
}
