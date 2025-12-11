using UnityEngine;

public class NorseGun : MonoBehaviour
{
    [SerializeField] private GunStats _stats;
    [SerializeField] private GunMagazine _magazine;

    public GunMagazine Magazine => _magazine;

    private GunFire _fire;
    private GunReload _reload;

    private void Awake()
    {
        _fire = GetComponent<GunFire>();
        _reload = GetComponent<GunReload>();
    }

    public void Initialize()
    {
        _reload.Initialize(_magazine, _stats);
        _fire.Initialize(_stats);
    }

    public void TryFire()
    {
        if(!_fire.IsReady || !_magazine.BulletCount.TryConsume(1)) return;
        _fire.Fire();
    }

    public void TryReload()
    {
        if (_magazine.IsFull) return;
        _reload.TryReload();
    }
}
