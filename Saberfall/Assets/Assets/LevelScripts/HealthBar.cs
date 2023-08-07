using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHleath(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
    }

    public void SetHleath(float health)
    {
        _healthSlider.value = health;
    }
}
