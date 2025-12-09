using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("체력 UI")]
    [SerializeField] private Slider _hpSliderUI;

    [Header("스태미나 UI")]
    [SerializeField] private Slider _staminaSliderUI;

    [Header("폭탄 UI")]
    [SerializeField] private Text _bombCountTexTUI;

    private void Start()
    {
        PlayerStats.Instance.Health.AddListener(UpdateHealthUI);
        PlayerStats.Instance.Stamina.AddListener(UpdateStaminaUI);
        PlayerStats.Instance.BombCount.AddListener(UpdateBombCountUI);
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.Health.RemoveListener(UpdateHealthUI);
        PlayerStats.Instance.Stamina.RemoveListener(UpdateStaminaUI);
        PlayerStats.Instance.BombCount.RemoveListener(UpdateBombCountUI);
    }
    
    private void UpdateHealthUI(float health, float maxHealth)
    {
        _hpSliderUI.value = health / maxHealth;
    }

    private void UpdateStaminaUI(float stamina, float maxStamina)
    {
        _staminaSliderUI.value = stamina / maxStamina;
    }

    private void UpdateBombCountUI(int bombCount)
    {
        _bombCountTexTUI.text = bombCount.ToString();
    }
}
