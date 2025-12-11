using System;
using System.Collections;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    private GunMagazine _magazine;
    private float _reloadTime;

    private CountStat _ownerMagazine;

    private bool _isReloading = false;

    public void Initialize(GunMagazine magazine, GunStats stats, GameObject onwer)
    {
        _magazine = magazine;
        _reloadTime = stats.ReloadTime.Value;
        _ownerMagazine = onwer.GetComponent<PlayerStats>().TotalBulletCount;
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
