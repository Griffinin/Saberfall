using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 10000;
    [SerializeField] private int _maxHealth = 10000;
    [SerializeField] private bool _IsAlive = true;
    //public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();


    }

    //sets the health
    public int Health
    {
        get { return _currentHealth; }
        set { 
            _currentHealth = value;
            healthChanged?.Invoke(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }
    //getter and setters for IsAlive
    public bool IsAlive
    {
        get
        {
            return _IsAlive;
        }
        set { _IsAlive = value;
            Debug.Log("IsAlive set " + value);
            anim.SetBool("isAlive", value);
        }
    }
    //getter and setters for isHit
    public bool IsHit
    {
        get
        {
            return anim.GetBool("isHit");
        }
        private set
        {
            anim.SetBool("isHit", value);
        }
    }

    public UnitHealth(int health, int maxhealth)
    {
        _currentHealth = health;
        _maxHealth = maxhealth;
    }


    //damages the player by subtracting health
    public bool DamageUnit(int damageAmount, Vector2 knockback)
    {
        if (IsAlive)
        {
            Health -= damageAmount;
            IsHit = true;
            // damageableHit?.Invoke(damageAmount, knockback);
            return true;

        }

        return false;
    }

    //healing 
    public void HealUnit(int healAmount)
    {
        if (_currentHealth + healAmount > _maxHealth) _currentHealth = _maxHealth;
        else _currentHealth += healAmount;
    }

    private void Update() {
        //DamageUnit(100); 
    
    
    }
}
