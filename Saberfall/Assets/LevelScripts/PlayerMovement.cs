using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb; // Player
    private int onGround;
    [SerializeField] private int MAX_JUMPS = 2;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        onGround = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if (dirX < 0)
            playerSprite.flipX = true;
        else playerSprite.flipX = false;

        if(Input.GetButtonDown("Jump") && onGround < MAX_JUMPS) {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            onGround++;
        }
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
