public class UnitHealth
{
    private int _currentHealth;
    private int _maxHealth;

    public int Health
    {
        get { return _currentHealth; }
        private set { _currentHealth = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }

    public UnitHealth(int health, int maxhealth)
    {
        _currentHealth = health;
        _maxHealth = maxhealth;
    }

    public void DamageUnit(int damageAmount)
    {
        if (_currentHealth > 0) _currentHealth -= damageAmount;
    }

    public void HealUnit(int healAmount)
    {
        if (_currentHealth + healAmount > _maxHealth) _currentHealth = _maxHealth;
        else _currentHealth += healAmount;
    }
}
