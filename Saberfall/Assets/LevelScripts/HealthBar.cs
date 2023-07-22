using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHleath(int maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
    }

    public void SetHleath(int health)
    {
        _healthSlider.value = health;
    }
}
