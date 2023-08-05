using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 10000;
    [SerializeField] private int _maxHealth = 10000;
    [SerializeField] private bool _IsAlive = true;
    //public UnityEvent<int, Vector2> damageableHit;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public int Health
    {
        get { return _currentHealth; }
        private set { 
            _currentHealth = value;
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

    public UnitHealth(int health, int maxhealth)
    {
        _currentHealth = health;
        _maxHealth = maxhealth;
    }


    //todo: adding knockback?
    public bool DamageUnit(int damageAmount, Vector2 knockback)
    {
        if (IsAlive)
        {
            Health -= damageAmount;
           // damageableHit?.Invoke(damageAmount, knockback);
            return true;
        }
        return false;
    }

    public void HealUnit(int healAmount)
    {
        if (_currentHealth + healAmount > _maxHealth) _currentHealth = _maxHealth;
        else _currentHealth += healAmount;
    }

    private void Update() {
        //DamageUnit(100); 
    
    
    }
}
