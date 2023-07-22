using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb; // Player
    private int onGround;
    private int MAX_JUMPS = 2;
    private bool jumpStarted = false;

    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private string currState;
    private bool canMove = true;
    private float previousVelocityY;

    const string IDLE = "Idle";
    const string RUN = "Run";
    const string JUMP1 = "Jump";
    const string JUMP2 = "Jump1";

    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    //public Transform groundPos;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform groundPos;
    private bool isGrounded;
    
    
    private bool jumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        onGround = 0;
        previousVelocityY = rb.velocity.y;
    }

    // Update is called once per frame
    private void Update()
    {
        previousVelocityY = rb.velocity.y;
        //isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, jumpableGround);
        isGrounded = IsGrounded();
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && onGround < MAX_JUMPS) {
            anim.SetTrigger("takeOff");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onGround++;
            jumping = true;
        }

        UpdateAnimationState(dirX);
    }

    private void changeAnimation(string newState)
    {
        if(currState == newState)
        {
            return;
        }
        anim.Play(newState);

        currState = newState;
    }

    private void UpdateAnimationState(float dirX)
    {

        //jumping
        if (jumping)
        {
            if (rb.velocity.y > .1f)
            {
                anim.SetBool("jumping", true);
                //state = MovementState.jumping;

            }
            else if (rb.velocity.y < -.1f)
            {

                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
                //state = MovementState.falling;
            }
            /* else if (rb.velocity.y < -.1f && onGround!=0)
             {

                 anim.SetBool("down", true);
                 //state = MovementState.falling;
             }*/
            // MovementState state;

            else if (isGrounded)
            {

                anim.SetBool("falling", false);
                jumping = false;
            }
        }



        //running
        if (dirX > 0f)
        {
            //state = MovementState.running;
            sprite.flipX = false;
            anim.SetBool("running", true);
            //changeAnimation(RUN);
        }
        else if (dirX < 0f)
        {
            //state = MovementState.running;
            sprite.flipX = true;
            anim.SetBool("running", true);
            //changeAnimation(RUN);
        }

        else
        {
            // anim.SetBool("jumping", false);
            //anim.SetBool("falling", false);
            anim.SetBool("running", false);
            //state = MovementState.idle;
            //changeAnimation(IDLE);
        }

        if (rb.velocity.y < -0.5f)
        {

            anim.SetBool("down", true);
            //anim.SetBool("falling", true);
            //state = MovementState.falling;
        }
        else if (isGrounded)
        {

            //anim.SetBool("falling", false);
            anim.SetBool("down", false);
        }

        //anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPos.position, checkRadius, ground);
    }

    // Checks if the this player object is colliding with other objects.
    // Performs computation if they are colliding.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set onGround to 0 when in contact with the ground so player can jump again
        if(collision.gameObject.name == "Terrain") 
            if (onGround != 0) onGround = 0;
    }
}
