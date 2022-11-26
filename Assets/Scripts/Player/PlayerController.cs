using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour {
    private bool holding, tapping = false;
    private float tapTime = 0;
    private float tapDuration = .2f;
    private float dashSpeed = 20f;
    private PlayerStatus status;
    private Collider2D playerCollider;
    private Rigidbody2D playerRB;
    public float movementSpeed;
    private Vector2 direction = Vector2.right;

    Camera mainCam;

    [SerializeField] private Vector2 movementInput;
    private bool grounded;
    [SerializeField] private float jumpForce;
    private bool onPassableGround;
    private Collider2D passableGround;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        mainCam = Camera.main;
        status = GetComponent<PlayerStatus>();
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        // Update is called once per frame
        Assert.IsNotNull(playerRB);
        Assert.IsNotNull(playerCollider);
        Assert.IsNotNull(animator);

        Assert.IsNotNull(status);
    }

    void Update() {
        if (!status.IsDead()) {
            Move();
        }
        if(tapping) {
            tapTime -= Time.deltaTime;
            if(tapTime <= 0) {
                tapping = false;
                holding = false;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        if(context.canceled && holding) {
            if(movementInput.x == 0) {
                holding = false;
                tapping = true;
            }
        }

        if(context.started || context.canceled) return;
        if(direction.x > 0 && movementInput.x < 0) TurnAround();
        if(direction.x < 0 && movementInput.x > 0) TurnAround();

        if(movementInput.x > 0 || movementInput.x < 0) {
            if(tapping) {
                tapping = false;
                Dash();
            }
            else {
                holding = true;
                tapTime = tapDuration;
            }
        }
        
        if (movementInput.y > 0) Jump();
        if (movementInput.y < 0) {
            Crouch();
        }
    }

    private void TurnAround () {
        direction *= -1;
        transform.localScale = new Vector2(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        holding = false;
        tapping = false;
    }

    public Vector2 GetDirection() {
        return direction;
    }

    public void Crouch() {
        if (status.IsDead()) return;
        if (onPassableGround) {
            Physics2D.IgnoreCollision(playerCollider, passableGround, true);
        }
    }

    private void Move() {
        Vector2 position = transform.position;
        position.x += movementSpeed * Time.deltaTime * movementInput.x;
        float leftOfScreen = mainCam.ScreenToWorldPoint(Vector3.zero).x;
        float rightOfScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;

        position.x = Mathf.Clamp(position.x, leftOfScreen, rightOfScreen);
        transform.position = position;
        animator.SetBool("IsMoving", Mathf.Abs(movementInput.x) > 0);
    }

    private void Dash()
    {
        if(status.IsDead()) return;
        playerRB.velocity = Vector2.zero;
        playerRB.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
    }

    private void Jump() {
        Vector2 jump = Vector2.zero;

        if (!status.IsDead() && grounded) {
            jump = Vector2.up * jumpForce;
            SetGrounded(false);
        }

        playerRB.AddForce(jump, ForceMode2D.Impulse);
    }

    public void Respawn() {
        transform.position = mainCam.ScreenToWorldPoint(new Vector2(Screen.width / 5, (Screen.height * 4) / 5));
    }

    private void SetGrounded(bool grounded) {
        this.grounded = grounded;
    }

    //Change to use Ground Collider?
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Ground")) {
            SetGrounded(true);
        }

        if (other.gameObject.tag.Equals("PassableGround")) {
            if (AboveCollider(other)) {
                grounded = true;
                onPassableGround = true;
                passableGround = other.collider;
            }
            else {
                Physics2D.IgnoreCollision(playerCollider, other.collider, true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("PassableGround")) {
            onPassableGround = false;
            Physics2D.IgnoreCollision(playerCollider, other, false);
        }
    }
    

    private bool AboveCollider(Collision2D other) {
        float playerFootPos = (transform.position.y + playerCollider.bounds.size.y) * transform.lossyScale.y;
        float colliderPos = other.gameObject.transform.position.y;
        return playerRB.velocity.y <= 0 &&
            playerFootPos >= colliderPos;
    }
}


