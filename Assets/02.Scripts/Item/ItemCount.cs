using System;
using UnityEngine;

[Serializable]
public class ItemCount
{
    [SerializeField] private int _count;

    private event Action<int> _onCountChanged;

    public int Count
    {
        get { return _count; }
        private set
        {
            _count = value;
            _onCountChanged?.Invoke(_count);
        }
    }

    public void Initialize()
    {
        Count = _count;
    }

    public void AddListener(Action<int> listener)
    {
        _onCountChanged += listener;
    }

    public void RemoveListener(Action<int> listener)
    {
        _onCountChanged -= listener;
    }

    public bool TryConsume(int count)
    {
        if (count <= 0 || _count < count) return false;

        Count -= count;
        return true;
    }

    public void Increase(int count)
    {
        if (count < 0) return;
        Count += count;
    }

    public void Decrease(int count)
    {
        if (count < 0) return;
        Count -= count;
    }
}
