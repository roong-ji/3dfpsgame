using UnityEngine;

[System.Serializable]
public struct RangeValue
{
    private float _value;

    public float Value
    {
        get => _value;
        set => _value = Mathf.Clamp(value, Min, Max);
    }

    public float Min;
    public float Max;
}