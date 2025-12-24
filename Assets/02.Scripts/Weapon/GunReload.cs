using System;
using System.Collections;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    private GunMagazine _magazine;
    private float _reloadTime;

    private IResource _ownerMagazine;

    private bool _isReloading = false;
    
    public bool IsReloading => _isReloading;

    private event Action<float> _onReloadProgress;

    public void Initialize(GunMagazine magazine, GunStats stats, GameObject owner)
    {
        _magazine = magazine;
        _reloadTime = stats.ReloadTime.Value;
        _ownerMagazine = owner.GetComponent<PlayerStats>().Bullet;
    }

    public void TryReload()
    {
        if (_isReloading) return;
        StartCoroutine(ReloadRoutine());
    }

    private void Reload()
    {
        int totalBullet = _ownerMagazine.Count;
        int bulletToFill = Mathf.Min(_magazine.NeedToFill, totalBullet);

        if (bulletToFill <= 0) return;

        _ownerMagazine.TryConsume(bulletToFill);
        _magazine.Bullet.Increase(bulletToFill);
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        _onReloadProgress?.Invoke(0);

        float timer = 0;

        while (timer < _reloadTime)
        {
            timer += Time.deltaTime;
            _onReloadProgress?.Invoke(timer / _reloadTime);
            yield return null;
        }

        _onReloadProgress?.Invoke(1);
        Reload();
        _isReloading = false;
    }

    public void AddListener(Action<float> listener)
    {
        _onReloadProgress += listener;
    }

    public void RemoveListener(Action<float> listener)
    {
        _onReloadProgress -= listener;
    }
}
