using System;
using UnityEngine;

[RequireComponent(typeof(GunFire), typeof(GunReload))]
public class NorseGun : MonoBehaviour
{
    [SerializeField] private GunStats _stats;
    [SerializeField] private GunMagazine _magazine;

    public GunMagazine Magazine => _magazine;
    public event Action<float> OnReloadProgress
    {
        add { _reload.AddListener(value); }
        remove { _reload.RemoveListener(value); }
    }

    private GameObject _owner;
    private GunFire _fire;
    private GunReload _reload;

    private void Awake()
    {
        _fire = GetComponent<GunFire>();
        _reload = GetComponent<GunReload>();
    }

    public void Initialize(GameObject owner)
    {
        _owner = owner;
        _reload.Initialize(_magazine, _stats, _owner);
        _fire.Initialize(_stats, _owner);
    }
    
    public bool TryFire()
    {
        if(!_fire.IsReady || !_magazine.Bullet.TryConsume(1)) return false;
        _fire.Fire();
        return true;
    }

    public void TryReload()
    {
        if (_magazine.IsFull) return;
        _reload.TryReload();
    }
}
