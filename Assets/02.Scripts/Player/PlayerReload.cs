using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    private const int Capacity = 30;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;
        Reload();
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
}
