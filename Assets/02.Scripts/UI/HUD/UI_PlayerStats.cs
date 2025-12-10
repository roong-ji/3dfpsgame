using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    [Header("체력 UI")]
    [SerializeField] private Slider _hpSliderUI;

    [Header("스태미나 UI")]
    [SerializeField] private Slider _staminaSliderUI;

    [Header("폭탄 UI")]
    [SerializeField] private Text _bombCountTexTUI;

    [Header("총알 UI")]
    [SerializeField] private Text _bulletCountTexTUI;

    private void Start()
    {
        PlayerStats.Instance.Health.AddListener(UpdateHealthUI);
        PlayerStats.Instance.Stamina.AddListener(UpdateStaminaUI);
        PlayerStats.Instance.BombCount.AddListener(UpdateBombCountUI);
        PlayerStats.Instance.BulletCount.AddListener(UpdateBulletCountUI);
        PlayerStats.Instance.TotalBulletCount.AddListener(UpdateBulletCountUI);
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance == null) return;
        PlayerStats.Instance.Health.RemoveListener(UpdateHealthUI);
        PlayerStats.Instance.Stamina.RemoveListener(UpdateStaminaUI);
        PlayerStats.Instance.BombCount.RemoveListener(UpdateBombCountUI);
        PlayerStats.Instance.BulletCount.RemoveListener(UpdateBulletCountUI);
        PlayerStats.Instance.TotalBulletCount.RemoveListener(UpdateBulletCountUI);
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

    private void UpdateBulletCountUI(int count)
    {
        int bulletCount = PlayerStats.Instance.BulletCount.Count;
        int maxBulletCount = PlayerStats.Instance.TotalBulletCount.Count;

        _bulletCountTexTUI.text = $"{bulletCount}/{maxBulletCount}";
    }

}
