using System;

public interface ICountStat
{
    int Count { get; }

    bool IsEmpty { get; }

    bool TryConsume(int count);

    void AddListener(Action<int> listener);
    void RemoveListener(Action<int> listener);
}
