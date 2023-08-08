using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] HealthBar _healthBar;

    void Start()
    {
        
    }

    void Update()
    {
      /*  if(Input.GetKeyDown(KeyCode.Q))
        {
            //PlayerTakeDamage(100);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerHeal(100);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }*/
    }

   private void PlayerTakeDamage(float health)
    {
       // GameManager.gameManager._playerHealth.DamageUnit(damageAmount);
        _healthBar.SetHleath(GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerHeal(int healAmmount)
    {
        GameManager.gameManager._playerHealth.HealUnit(healAmmount);
        _healthBar.SetHleath(GameManager.gameManager._playerHealth.Health);
    }
}
