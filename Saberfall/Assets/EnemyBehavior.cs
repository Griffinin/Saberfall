
using UnityEngine;

/// <summary>
/// Controls the behavior of the enemy, including movement, patrolling, attacking, and interaction with the player.
/// </summary>

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damageAmount = 10;

    [SerializeField] LayerMask groundLayer; //make sure layer is assigned for enemy
    private bool isGrounded;

    private bool movingRight = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] HealthBar _healthBar;
    [SerializeField] EnemyHealth _enemyHealth;

    [SerializeField] private Vector2 defaultKnockback = new Vector2(0, 0);

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float patrolRadius = 5f;
    private float timeSinceLastAttack = 0;
    private Transform player;
    public PatrolPath path;
    internal PatrolPath.Mover mover;
    private bool flipping = true;



    /// <summary>
    /// Initializes required components and properties for the enemy behavior.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        _enemyHealth = new EnemyHealth(100, 100);
        
        //todo Add when health set for player

        if (path != null)
        {
            mover = path.CreateMover(moveSpeed);
        }


        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    /// <summary>
    /// Indicates if the enemy can move or not based on its current animation state.
    /// </summary>
    public bool CanMove
    {
        get

        {
            return anim.GetBool("canMove");
        }
    }

    /// <summary>
    /// Updates the enemy's behavior including movement, patrolling, and attacking based on its proximity to the player.
    /// </summary>
    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        if (!isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within the patrol radius
        if (distanceToPlayer <= patrolRadius)
        {
            if (distanceToPlayer < attackRange)
            {
                if (timeSinceLastAttack > attackCooldown)
                {
                    Attack();
                    timeSinceLastAttack = 0;
                }
            }
            else if (mover != null && CanMove) // Patrol logic
            {
                Vector2 newPosition = mover.Position;
                movingRight = newPosition.x > transform.position.x;
                transform.position = newPosition;

                sprite.flipX = !movingRight; // flip the enemy sprite based on direction


                anim.SetTrigger("enemyRun");
            }
        }
        else if (mover != null && CanMove) // If the player is not within the patrol radius, continue patrolling.
        {
            Vector2 newPosition = mover.Position;
            movingRight = newPosition.x > transform.position.x;
            transform.position = newPosition;

            sprite.flipX = !movingRight; // flip the enemy sprite based on direction


            anim.SetTrigger("enemyRun");
        }
        timeSinceLastAttack += Time.deltaTime;



    }

    /// <summary>
    /// Detects collisions with other game objects, specifically the player, to inflict damage.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.gameManager._playerHealth.DamageUnit(damageAmount, defaultKnockback);
        }
    }

    /// <summary>
    /// Changes the enemy's animation state.
    /// </summary>
    private void ChangeAnimationState(string newState)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(newState)) return;

        anim.Play(newState);
    }

    /// <summary>
    /// Triggers the enemy's attack action.
    /// </summary>
    private void Attack()
    {

        anim.SetTrigger("enemyAttack");
        // Check if player is in range
    }

    /// <summary>
    /// Visualizes the enemy's attack range within the Unity Editor using gizmos.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    /// <summary>
    /// Destroys the enemy game object.
    /// </summary>
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }


    /// <summary>
    /// Flips the enemy's orientation.
    /// </summary>

    private void flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
        flipping = !flipping;
    }
}
