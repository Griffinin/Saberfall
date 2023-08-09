using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float dirX;
    private bool flipping = true;

    const string IDLE = "Idle";
    const string RUN = "Run";
    const string JUMP1 = "Jump";
    const string JUMP2 = "Jump1";

    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private int currLevel = 1;

    //public Transform groundPos;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform groundPos;
    private bool isGrounded;
    Checks checks;
    
    private bool jumping = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        checks = GetComponent<Checks>();
        onGround = 0;
        previousVelocityY = rb.velocity.y;
    }

    public bool IsAlive
    {
        get
        {
            return anim.GetBool("isAlive");
        }
        set  ///remove maybe
        {
            anim.SetBool("isAlive", value);
        }
    }

    public bool CanMove
    {
        get

        {
            return anim.GetBool("canMove");
        }
    }
    // Update is called once per frame
    private void Update()
    {
        previousVelocityY = rb.velocity.y;
        //isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, jumpableGround);
        isGrounded = IsGrounded();
        dirX = Input.GetAxisRaw("Horizontal");
        if (CanMove)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


            if (Input.GetButtonDown("Jump") && onGround < MAX_JUMPS)
            {
                anim.SetTrigger("takeOff");

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                onGround++;
                jumping = true;
            }
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


        
        if (checks.grounded)
        {

            //anim.SetBool("falling", false);
            anim.SetBool("down", false);
        }
        else if (rb.velocity.y < -0.8f)
        {

            anim.SetBool("down", true);
        }
        //jumping
        // if (jumping)
        //{
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

            else if (checks.grounded)
            {
                anim.ResetTrigger("takeOff");
                anim.SetBool("falling", false);
                jumping = false;

        }
        //}




        //running
        if (IsAlive)
        {
            if (dirX > 0f && !flipping)
            {
                flip();

            }
            else if (dirX < 0f && flipping)
            {
                flip();

            }
        }

        if (dirX != 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (!IsAlive)
        {
            if(currLevel == 0)
            {
                Invoke("ReloadLevel1", 0.1f);
                IsAlive = true;
            }
            else if (currLevel == 1)
            {
                Invoke("ReloadLevel2", 0.1f);
                IsAlive = true;
            }
        }


 
        //anim.SetInteger("State", (int)state);
    }

    private void ReloadLevel1()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //change to Find instead
        SceneManager.UnloadSceneAsync("RemoveThisLev1");
        SceneManager.LoadSceneAsync("RemoveThisLev1", LoadSceneMode.Additive);

    }


    private void ReloadLevel2()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //change to Find instead
        //SceneManager.UnloadSceneAsync("RemoveThisLev2");
        SceneManager.UnloadSceneAsync("MainChar-scene");
        SceneManager.LoadSceneAsync("MainChar-scene", LoadSceneMode.Additive);

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
        if(collision.gameObject.name == "first2") 
            if (onGround != 0) onGround = 0;
    }

    private void flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
        flipping = !flipping;
    }

    public bool getFlip()
    {
        return flipping;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y+ knockback.y);
    }
}
