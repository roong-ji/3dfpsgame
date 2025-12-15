using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Monster))]
public class MonsterHealthBar : MonoBehaviour
{
    private Monster _monster;

    [SerializeField] private Image _gaugeImage;
    [SerializeField] private Transform _healthBarTransform;
    private Transform _mainCameraTransform;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
        _monster.Health.AddListener(UpdateHealthUI);
        _mainCameraTransform = Camera.main.transform;
    }

    private void OnDestroy()
    {
        _monster.Health.RemoveListener(UpdateHealthUI);
    }

    private void LateUpdate()
    {
        // 빌보드 기법
        if (_monster.IsDead) return;
        _healthBarTransform.transform.forward = _mainCameraTransform.forward;
    }

    private void UpdateHealthUI(float health, float maxHealth)
    {
        _gaugeImage.fillAmount = health / maxHealth;
    }
}
