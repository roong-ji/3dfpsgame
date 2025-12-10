using System;
using System.Collections;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    private const int Capacity = 30;
    private const float ReloadTime = 1.6f;

    private bool _isReloading = false;

    private static event Action<float> _onReloadProgress;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R) || _isReloading) return;
        StartCoroutine(ReloadRoutine());
    }

    private void Reload()
    {
        int totalBulelt = PlayerStats.Instance.TotalBulletCount.Count;
        int currentBullet = PlayerStats.Instance.BulletCount.Count;

        int needToFill = Capacity - currentBullet;
        int bulletToFill = Mathf.Min(needToFill, totalBulelt);

        if (bulletToFill <= 0) return;

        PlayerStats.Instance.TotalBulletCount.TryConsume(bulletToFill);
        PlayerStats.Instance.BulletCount.Increase(bulletToFill);
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        _onReloadProgress?.Invoke(0);

        float timer = 0;

        while (timer < ReloadTime)
        {
            timer += Time.deltaTime;
            _onReloadProgress?.Invoke(timer / ReloadTime);
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
