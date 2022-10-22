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

    Camera mainCam;

    [SerializeField] private Vector2 movementInput;
    private bool grounded;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start() {
        mainCam = Camera.main;
        status = GetComponent<PlayerStatus>();
        playerRB = GetComponent<Rigidbody2D>();
        
    // Update is called once per frame
        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(groundCollider);

        Assert.IsNotNull(status);
    }

    void Update() {
        if(!status.IsDead()) {
            Move();
        }
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


    private void Move() {
        Vector2 position = transform.position;
        position.x += movementSpeed * Time.deltaTime * movementInput.x;
        float leftOfScreen = mainCam.ScreenToWorldPoint(Vector3.zero).x;
        float rightOfScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;

        position.x = Mathf.Clamp(position.x, leftOfScreen, rightOfScreen);
        transform.position = position;
    }

    private void Jump() {
        Vector2 jump = Vector2.zero;

        if(!status.IsDead() || grounded) {
            jump = Vector2.up * jumpForce;
            SetGrounded(false);
        }

        playerRB.AddForce(jump, ForceMode2D.Impulse);
    }

    public void Respawn() {
        transform.position = mainCam.ScreenToWorldPoint(new Vector2(Screen.width/5, (Screen.height * 4)/5));
    }

    private void SetGrounded(bool grounded) {
        this.grounded = grounded;
    }

    //Change to use Ground Collider?
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Ground") || other.gameObject.tag.Equals("PassableGround")) {
            SetGrounded(true);
        }
    }
}
