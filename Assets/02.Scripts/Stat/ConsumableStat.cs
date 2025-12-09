using System;
using UnityEngine;

[Serializable]
public class ConsumableStat
{
    [SerializeField] private float _maxValue = 100f;
    [SerializeField] private float _regenerationRate = 10f;
    private float _value;

    public float Value
    {
        get { return _value; }
        private set
        {
            _value = Mathf.Clamp(value, 0, _maxValue);
        }
    }

    public void Initialize()
    {
        Value = _maxValue;
    }

    public void Regenerate(float time)
    {
        Value += _regenerationRate * time;
    }

    public bool TryConsume(float cost)
    {
        if (cost <= 0 || _value < cost) return false;

        Value -= cost;
        return true;
    }

    public void IncreaseMax(float amount)
    {
        if (amount < 0) return;
        _maxValue += amount;
    }

    public void DecreaseMax(float amount)
    {
        if (amount < 0) return;
        _maxValue -= amount;
    }

    public void Increase(float amount)
    {
        if (amount < 0) return;
        Value += amount;
    }

    public void Decrease(float amount)
    {
        if (amount < 0) return;
        Value -= amount;
    }
}
