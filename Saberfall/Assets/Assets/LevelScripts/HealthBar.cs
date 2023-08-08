using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;
    UnitHealth playerHeal;
    private void Awake()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHeal = player.GetComponent<UnitHealth>();
    }

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();


    }

    private void OnEnable()
    {
        playerHeal.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerHeal.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    public void SetMaxHleath(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
    }

    public void SetHleath(float health)
    {
        _healthSlider.value = health;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        SetHleath(newHealth);
    }
}
