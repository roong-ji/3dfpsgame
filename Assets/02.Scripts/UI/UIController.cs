using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("체력 UI")]
    [SerializeField] private Slider _hpSliderUI;

    [Header("스태미나 UI")]
    [SerializeField] private Slider _staminaSliderUI;

    private void Awake()
    {
        PlayerStamina.OnStaminaChanged += UpdateStaminaUI;
    }

    private void OnDestroy()
    {
        PlayerStamina.OnStaminaChanged -= UpdateStaminaUI;
    }

    private void UpdateStaminaUI(float stamina, float maxStamina)
    {
        _staminaSliderUI.value = stamina / maxStamina;
    }
}
