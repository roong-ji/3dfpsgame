using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("최대 스태미나")]
    [SerializeField] private float _maxStamina = 100f;
    private float _stamina;

    [Header("초당 스태미나 회복량")]
    [SerializeField] private float _staminaRegenRate = 10f;

    public static event Action<float, float> OnStaminaChanged;

    public float Stamina
    {
        get { return _stamina; } 
        private set 
        {
            float newValue = Mathf.Clamp(value, 0, _maxStamina);

            if (newValue != _stamina)
            {
                _stamina = newValue;
                OnStaminaChanged?.Invoke(_stamina, _maxStamina);
            }
        }
    }

    private void Start()
    {
        Stamina = _maxStamina;
    }

    private void Update()
    {
        RegenStamina();
    }

    private void RegenStamina()
    {
        Stamina += _staminaRegenRate * Time.deltaTime;
    }

    public bool TryUseStamina(float stamina)
    {
        if(stamina <=0 || _stamina < stamina) return false;

        Stamina -= stamina;
        return true;
    }
}
