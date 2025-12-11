using System;
using System.Collections;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    private GunMagazine _magazine;
    private float _reloadTime;

    private bool _isReloading = false;

    public void Initialize(GunMagazine magazine, GunStats stats)
    {
        _magazine = magazine;
        _reloadTime = stats.ReloadTime.Value;
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
        PlayerGunController.UpdateReloadProgress(0);

        float timer = 0;

        while (timer < _reloadTime)
        {
            timer += Time.deltaTime;
            PlayerGunController.UpdateReloadProgress(timer / _reloadTime);
            yield return null;
        }

        PlayerGunController.UpdateReloadProgress(1);
        Reload();
        _isReloading = false;
    }
}
