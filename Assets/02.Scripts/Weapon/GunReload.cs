using System;
using System.Collections;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    private GunMagazine _magazine;
    private float _reloadTime;

    private bool _isReloading = false;

    private static event Action<float> _onReloadProgress;

    public void Initialize(GunMagazine magazine, float reloadTime)
    {
        _magazine = magazine;
        _reloadTime = reloadTime;
    }

    public void TryReload()
    {
        if (_isReloading) return;
        StartCoroutine(ReloadRoutine());
    }

    private void Reload()
    {
        int totalBullet = PlayerStats.Instance.TotalBulletCount.Count;
        int bulletToFill = Mathf.Min(_magazine.NeedToFill, totalBullet);

        if (bulletToFill <= 0) return;

        PlayerStats.Instance.TotalBulletCount.TryConsume(bulletToFill);
        _magazine.BulletCount.Increase(bulletToFill);
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

    public static void AddListener(Action<float> listener)
    {
        _onReloadProgress += listener;
    }

    public static void RemoveListener(Action<float> listener)
    {
        _onReloadProgress -= listener;
    }
}
