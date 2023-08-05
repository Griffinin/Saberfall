
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damageAmount = 10;

    [SerializeField] LayerMask groundLayer; // This is used to detect the ground.
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
    private float timeSinceLastAttack = 0;
    private Transform player;
    public PatrolPath path;
    internal PatrolPath.Mover mover;
    

    // protected GroundedController2D groundedController2D;

    // Animation States
    private const string IDLE = "void_idle";
    private const string RUN = "void_run";
    private const string DEATH = "void_die";
    private const string ATTACK = "void_attack";

    private void Start()
    {
        //groundedController2D = GetComponent<GroundedController2D>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        _enemyHealth = new EnemyHealth(100, 100);
        
        //todo Add when health set for player
        //_healthBar.SetMaxHleath(_enemyHealth.MaxHealth);

        if (path != null)
        {
            mover = path.CreateMover(moveSpeed);
        }


        player = GameObject.FindGameObjectWithTag("Player").transform;


        ChangeAnimationState(IDLE); //todo initaliaze or replace with tag?

        //float widthScaleFactor = 202f / 37f;
        //float heightScaleFactor = 123f / 27f;
        //transform.localScale = new Vector3(widthScaleFactor, heightScaleFactor, 1f);

    }

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

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < attackRange)
            {
                if (timeSinceLastAttack > attackCooldown)
                {
                    Attack();
                    timeSinceLastAttack = 0;
                }
            }
        }
        else
        {
            // Here's the patrol logic:
            if (mover != null)
            {
                Vector2 newPosition = mover.Position;
                movingRight = newPosition.x > transform.position.x;
                transform.position = newPosition;

                sprite.flipX = !movingRight; // flip the enemy sprite based on direction
                ChangeAnimationState(RUN);
            }
        }
        timeSinceLastAttack += Time.deltaTime;
    }


    //private void Update()
    //{
    //    isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

    //    // If the enemy is not grounded, apply a gravity-like force.
    //    if (!isGrounded)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, -2);  
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, 0);  
    //    }

    //    //if (path != null)
    //    //{
    //    //    if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
    //    //    control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
    //    //}
    //    // movement
    //    if (movingRight)
    //    {
    //        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    //        sprite.flipX = false;
    //        ChangeAnimationState(RUN);
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    //        sprite.flipX = true;
    //        ChangeAnimationState(RUN);
    //    }

    //    //if (groundedController2D != null)
    //    //{
    //    //    groundedController2D.CheckCapsuleEndCollisions();
    //    //}
    //    //else
    //    //{
    //    //    Debug.LogWarning("groundedController2D is not initialized!");
    //    //}

    //    // Check for zero health
    //    if (_enemyHealth.Health <= 0)
    //    {
    //        ChangeAnimationState(DEATH);
    //        Destroy(gameObject, 1f);
    //    }

    //    // Attack logic
    //    //float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    //    //if (distanceToPlayer < attackRange && timeSinceLastAttack > attackCooldown)
    //    //{
    //    //    Attack();
    //    //    timeSinceLastAttack = 0;
    //    //}
    //    //timeSinceLastAttack += Time.deltaTime;

    //    if (player != null) //todo add better error handling
    //    {
    //        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    //        if (distanceToPlayer < attackRange)
    //        {
    //            if (timeSinceLastAttack > attackCooldown)
    //            {
    //                Attack();
    //                timeSinceLastAttack = 0;
    //            }
    //        }
    //        timeSinceLastAttack += Time.deltaTime;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{

        //    GameManager.gameManager._playerHealth.DamageUnit(damageAmount, defaultKnockback);
        //}
        //else if (collision.gameObject.CompareTag("Wall"))
        //{

        //    movingRight = !movingRight;
        //}
    }

    public void EnemyTakeDamage(int damage)
    {
        _enemyHealth.DamageEnemy(damage);
        _healthBar.SetHleath(_enemyHealth.Health);
    }

    private void ChangeAnimationState(string newState)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(newState)) return;

        anim.Play(newState);
    }

    private void Attack()
    {
        ChangeAnimationState(ATTACK);

        // Check if player is in range
        //Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange);
        //foreach (Collider2D player in hitPlayers)
        //{
        //    if (player.CompareTag("Player"))
        //    {
        //        GameManager.gameManager._playerHealth.DamageUnit(attackDamage);
        //    }
        //}

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D playerCollider in hitPlayers)
        {
            if (playerCollider.CompareTag("Player"))
            {
                GameManager.gameManager._playerHealth.DamageUnit(attackDamage, defaultKnockback);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
