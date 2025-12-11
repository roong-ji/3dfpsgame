using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("현재 장착 중인 총기")]
    [SerializeField] private NorseGun _gun;

    private static event Action<GunMagazine> _onGunEquipped;

    private void Start()
    {
        Equip(_gun);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GunFire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GunReload();
        }
    }

    private void GunFire()
    {
        if (_onGunEquipped == null) return;
        _gun.TryFire();
    }

    private void GunReload()
    {
        if (_onGunEquipped == null || PlayerStats.Instance.TotalBulletCount.IsEmpty) return;
        _gun.TryReload();
    }

    private void Equip(NorseGun gun)
    {
        _gun = gun;
        _onGunEquipped?.Invoke(gun.Magazine);
    }

    public static void AddListener(Action<GunMagazine> listener)
    {
        _onGunEquipped += listener;
    }

    public static void RemoveListener(Action<GunMagazine> listener)
    {
        _onGunEquipped -= listener;
    }
}
