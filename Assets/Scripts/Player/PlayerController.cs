using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour {
    private float dashSpeed = 20f;
    private PlayerStatus status;
    private Collider2D playerCollider;
    public Rigidbody2D playerRB;
    public float movementSpeed;
    private Vector2 direction = Vector2.right;
    private Collider2D enemyCollider;

    Camera mainCam;

    [SerializeField] private Vector2 movementInput;
    private bool grounded = true;
    [SerializeField] private float jumpForce;
    private bool onPassableGround;
    private Collider2D passableGround;
    private bool canWallJump = false;
    private Vector2 wallJumpDirection = Vector2.right;
    private float wallJumpDelay = 0.1f;
    private float wallJumpTime = 0f;
    private float wallJumpStrength = 0.5f;

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

            //Play player running sound
            //TO DO: fix it so it loops after playing the audio clip fully
            //if (grounded) FindObjectOfType<AudioManager>().Play("Run");
        }

        if(wallJumpTime > 0) {
            wallJumpTime -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();

        if(status.IsDead() || context.started || context.canceled) return;
        if(direction.x > 0 && movementInput.x < 0) TurnAround();
        if(direction.x < 0 && movementInput.x > 0) TurnAround();

        if (movementInput.y < 0) {
            Crouch();
        }
    }

    private void TurnAround () {
        direction *= -1;
        transform.localScale = new Vector2(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
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

    public void Dash()
    {
        if (status.IsDead()) return;
        playerRB.velocity = Vector2.zero;
        playerRB.AddForce(direction * dashSpeed, ForceMode2D.Impulse);

        //Play dash sound
        FindObjectOfType<AudioManager>().Play("Dash");
    }

    public void Jump() {
        Vector2 jump = Vector2.zero;

        if(status.IsDead()) return;
        if (!status.IsDead() && (canWallJump || wallJumpTime > 0)) {
            Vector2 jumpDirection = wallJumpDirection + Vector2.up; 
            jump = jumpDirection * jumpForce;
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            canWallJump = false;
            wallJumpTime = 0;
        }

        if (!status.IsDead() && grounded) {
            jump = Vector2.up * jumpForce;
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            grounded = false;
        }


        playerRB.AddForce(jump, ForceMode2D.Impulse);
        animator.SetBool("Jumped", true);
    }

    public void Respawn() {
        transform.position = mainCam.ScreenToWorldPoint(new Vector2(Screen.width / 5, (Screen.height * 4) / 5));
    }


    //Change to use Ground Collider?
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Ground")) {
            grounded = true;
            canWallJump = false;
            animator.SetBool("Jumped", false);
        }

        if(other.gameObject.tag.Equals("Walls")) {
            if(!grounded) {
                canWallJump = true;
                wallJumpTime = wallJumpDelay;
                wallJumpDirection.x = other.GetContact(0).point.x < transform.position.x? wallJumpStrength : -wallJumpStrength;
            }
        }

        if (other.gameObject.tag.Equals("Ceiling")){
            if (!grounded){

            }
        }

        if (other.gameObject.tag.Equals("PassableGround")) {
            if (AboveCollider(other.collider)) {
                grounded = true;
                onPassableGround = true;
                canWallJump = false;
                passableGround = other.collider;
                animator.SetBool("Jumped", false);
            }
            else {
                Physics2D.IgnoreCollision(playerCollider, other.collider, true);
            }
        }

        if (other.gameObject.tag.Equals("Enemy")){
            if(status.IsDead()){
                enemyCollider = other.collider;
                Physics2D.IgnoreCollision(playerCollider, other.collider, true);
            }
            else{
                Physics2D.IgnoreCollision(playerCollider, other.collider, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("PassableGround")) {
            onPassableGround = false;
            
            Physics2D.IgnoreCollision(playerCollider, other, false);

            if(!AboveCollider(other)) {
                grounded = false;
            }
        }
        if(other.gameObject.tag.Equals("Walls")) {
            canWallJump = false;
        }
    }
    

    private bool AboveCollider(Collider2D other) {
        float playerFootPos = (transform.position.y + playerCollider.bounds.size.y) * transform.lossyScale.y;
        float colliderPos = other.gameObject.transform.position.y;
        return playerRB.velocity.y <= 0 &&
            playerFootPos >= colliderPos;
    }
}


