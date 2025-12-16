using System;
using UnityEngine;

[Serializable]
public class ValueStat : IValueStat
{
    [SerializeField]
    private float _value;
    public float Value => _value;

    public void Increase(float amount)
    {
        if (amount < 0) return;
        _value += amount;
    }

    public void Decrease(float amount)
    {
        if (amount < 0) return;
        _value -= amount;
    }

    public void SetValue(float value)
    {
        _value = value;
    }
}
