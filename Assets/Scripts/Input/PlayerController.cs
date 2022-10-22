using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

//TODO LIST
//Camera Clamp

public class PlayerController : MonoBehaviour {
    private PlayerStatus status;
    private Rigidbody2D playerRB;
    [SerializeField] private Collider2D groundCollider;
    public float movementSpeed;

    [SerializeField] private Vector2 movementInput;
    private bool grounded;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start() {
        status = GetComponent<PlayerStatus>();
        playerRB = GetComponent<Rigidbody2D>();
        
        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(groundCollider);

        Assert.IsNotNull(status);
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        if(movementInput.y > 0) Jump();
        if(movementInput.y < 0) {
            Crouch();
        }
    }

    public void Crouch() {
        if(status.IsDead()) return;
        // if(onPassableGround) {
        //     Physics2D.IgnoreCollision(playerColl, passableGround, true);
        // }
    }

    //TODO: find a better way to turn back on collision
    public void ReleaseCrouch() {
        if(status.IsDead()) return;
        //Physics2D.IgnoreCollision(playerColl, passableGround, false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
    }

    private void Move() {
        Vector2 currentVelocity = playerRB.velocity;
        Vector2 targetVelocity = new Vector2(movementInput.x, 0);
        targetVelocity *= movementSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector2 velocityChange = (targetVelocity - currentVelocity);

        playerRB.AddForce(velocityChange, ForceMode2D.Force);
    }

    private void Jump() {
        Vector2 jump = Vector2.zero;

        if(grounded) {
            jump = Vector2.up * jumpForce;
            SetGrounded(false);
        }

        playerRB.AddForce(jump, ForceMode2D.Impulse);
    }

    private void SetGrounded(bool grounded) {
        this.grounded = grounded;
    }

    //Change to use Ground Collider?
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Ground")) {
            SetGrounded(true);
        }
    }
}
