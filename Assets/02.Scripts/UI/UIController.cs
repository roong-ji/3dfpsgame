using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("체력 UI")]
    [SerializeField] private Slider _hpSliderUI;

    [Header("스태미나 UI")]
    [SerializeField] private Slider _staminaSliderUI;

    private void Start()
    {
        PlayerStats.Instance.Health.OnValueChanged += UpdateHealthUI;
        PlayerStats.Instance.Stamina.OnValueChanged += UpdateStaminaUI;
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.Health.OnValueChanged -= UpdateHealthUI;
        PlayerStats.Instance.Stamina.OnValueChanged -= UpdateStaminaUI;
    }
    
    private void UpdateHealthUI(float health, float maxHealth)
    {
        _hpSliderUI.value = health / maxHealth;
    }

    private void UpdateStaminaUI(float stamina, float maxStamina)
    {
        _staminaSliderUI.value = stamina / maxStamina;
    }
}
