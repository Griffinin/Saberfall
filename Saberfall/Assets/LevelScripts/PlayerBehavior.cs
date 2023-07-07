using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] HealthBar _healthBar;
    [SerializeField] private bool Debugging = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerTakeDamage(10);
            if (Debugging) Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerHeal(10);
            if (Debugging) Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerTakeDamage(int damageAmount)
    {
        GameManager.gameManager._playerHealth.DamageUnit(damageAmount);
        _healthBar.SetHleath(GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerHeal(int healAmmount)
    {
        GameManager.gameManager._playerHealth.HealUnit(healAmmount);
        _healthBar.SetHleath(GameManager.gameManager._playerHealth.Health);
    }
}
