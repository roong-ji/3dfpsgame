using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("현재 장착 중인 총기")]
    [SerializeField] private NorseGun _gun;

    private static event Action<NorseGun> _onGunEquipped;

    private ICountStat _totalBulletCount;

    private void Start()
    {
        _totalBulletCount = GetComponent<PlayerStats>().TotalBulletCount;
        Equip(_gun);
    }

    private void Update()
    {
        if (!CursorManager.Instance.IsLockCursor || GameManager.Instance.AutoMode) return;

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
        if (_gun == null) return;
        _gun.TryFire();
    }

    private void GunReload()
    {
        if (_gun == null || _totalBulletCount.IsEmpty) return;
        _gun.TryReload();
    }

    private void Equip(NorseGun gun)
    {
        _gun = gun;
        _gun.Initialize(gameObject);
        _onGunEquipped?.Invoke(gun);
    }

    public static void AddListener(Action<NorseGun> listener)
    {
        _onGunEquipped += listener;
    }

    public static void RemoveListener(Action<NorseGun> listener)
    {
        _onGunEquipped -= listener;
    }
}
