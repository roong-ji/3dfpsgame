using System;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    [SerializeField] private Resource _gold;
    public IResource Gold => _gold;

    protected override void Init()
    {
        IsDestroyOnLoad = false;
        base.Init();
    }

    public void GainGold(int amount)
    {
        _gold.Increase(amount);
    }

    public void Bind(Action<int> action)
    {
        _gold.AddListener(action);
        _gold.Initialize();
    }

    public void UnBind(Action<int> action)
    {
        _gold.RemoveListener(action);
    }
}
