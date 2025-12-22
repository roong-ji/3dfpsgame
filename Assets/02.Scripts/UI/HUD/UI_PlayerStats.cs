using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    [Header("체력 UI")]
    [SerializeField] private Slider _hpSliderUI;

    [Header("스태미나 UI")]
    [SerializeField] private Slider _staminaSliderUI;

    [Header("폭탄 UI")]
    [SerializeField] private Text _bombCountTexTUI;

    private void Awake()
    {
        _playerStats.Health.AddListener(UpdateHealthUI);
        _playerStats.Stamina.AddListener(UpdateStaminaUI);
        _playerStats.Bomb.AddListener(UpdateBombCountUI);
    }

    private void OnDestroy()
    {
        if (_playerStats == null) return;
        _playerStats.Health.RemoveListener(UpdateHealthUI);
        _playerStats.Stamina.RemoveListener(UpdateStaminaUI);
        _playerStats.Bomb.RemoveListener(UpdateBombCountUI);
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
