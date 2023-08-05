using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damageAmount = 10;

    private bool movingRight = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] HealthBar _healthBar;
    [SerializeField] EnemyHealth _enemyHealth;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackCooldown = 1f;
    private float timeSinceLastAttack = 0;
    private Transform player;

    // Animation States
    private const string IDLE = "void_idle";
    private const string RUN = "void_run";
    private const string DEATH = "void_die";
    private const string ATTACK = "void_attack";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        _enemyHealth = new EnemyHealth(100, 100);
        _healthBar.SetMaxHleath(_enemyHealth.MaxHealth);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // movement
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            sprite.flipX = false;
            ChangeAnimationState(RUN);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            sprite.flipX = true;
            ChangeAnimationState(RUN);
        }

        // Check for zero health
        if (_enemyHealth.Health <= 0)
        {
            ChangeAnimationState(DEATH);
            Destroy(gameObject, 1f);
        }

        // Attack logic
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //if (distanceToPlayer < attackRange && timeSinceLastAttack > attackCooldown)
        //{
        //    Attack();
        //    timeSinceLastAttack = 0;
        //}
        //timeSinceLastAttack += Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            if (timeSinceLastAttack > attackCooldown)
            {
                Attack();
                timeSinceLastAttack = 0;
            }
        }
        timeSinceLastAttack += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.gameManager._playerHealth.DamageUnit(damageAmount);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {

            movingRight = !movingRight;
        }
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
                GameManager.gameManager._playerHealth.DamageUnit(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
