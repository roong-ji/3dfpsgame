using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("최대 체력")]
    [SerializeField] private float _maxHealth = 100f;
    private float _health;

    [Header("초당 체력 회복량")]
    [SerializeField] private float _healthRegenRate = 1f;

    public static event Action<float, float> OnHealthChanged;
    
    public float Health
    {
        get { return _health; }
        private set
        {
            float newValue = Mathf.Clamp(value, 0, _maxHealth);

            if (newValue != _health)
            {
                _health = newValue;
                OnHealthChanged?.Invoke(_health, _maxHealth);
            }
        }
    }

    private void Start()
    {
        Health = _maxHealth;
    }

    private void Update()
    {
        RegenHealth();
    }

    private void RegenHealth()
    {
        Health += _healthRegenRate * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;
        Health -= damage;
    }
}
