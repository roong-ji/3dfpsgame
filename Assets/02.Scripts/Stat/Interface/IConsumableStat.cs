using System;

public interface IConsumableStat
{
    float Value { get; }
    float MaxValue { get; }

    bool TryConsume(float amount);

    void AddListener(Action<float, float> listener);
    void RemoveListener(Action<float, float> listener);
}