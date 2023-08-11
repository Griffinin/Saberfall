// NOTE: Consider marking this class with [Obsolete] attribute if it's not being used anymore.
/// <summary>
/// Represents the health system for an enemy. This class may be obsolete.
/// </summary>
public class EnemyHealth
{
    private int _currentHealth;
    private int _maxHealth;

    /// <summary>
    /// Gets the current health of the enemy.
    /// </summary>
    public int Health
    {
        get { return _currentHealth; }
        private set { _currentHealth = value; }
    }

    /// <summary>
    /// Gets the maximum health the enemy can have.
    /// </summary>
    public int MaxHealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyHealth"/> class with the specified health and max health values.
    /// </summary>
    /// <param name="health">Initial health of the enemy.</param>
    /// <param name="maxHealth">Maximum possible health of the enemy.</param>
    public EnemyHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }

    /// <summary>
    /// Inflicts damage to the enemy. Reduces the enemy's current health.
    /// </summary>
    /// <param name="damageAmount">Amount of damage to inflict.</param>
    public void DamageEnemy(int damageAmount)
    {
        if (_currentHealth > 0) _currentHealth -= damageAmount;
    }

    /// <summary>
    /// Heals the enemy by the specified amount without exceeding the max health.
    /// </summary>
    /// <param name="healAmount">Amount to heal the enemy.</param>
    public void HealEnemy(int healAmount)
    {
        if (_currentHealth + healAmount > _maxHealth) _currentHealth = _maxHealth;
        else _currentHealth += healAmount;
    }
}
