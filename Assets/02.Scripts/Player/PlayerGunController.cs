using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("현재 장착 중인 총기")]
    [SerializeField] private NorseGun _gun;
    private static event Action<NorseGun> _onGunEquipped;

    private EZoomMode _zoomMode = EZoomMode.Normal;
    public static event Action<EZoomMode> OnZoomModeChanged;

    private IResource _totalBulletCount;
    private PlayerAnimator _animator;
    private PlayerStats _stats;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _stats = GetComponent<PlayerStats>();
        _totalBulletCount = _stats.Bullet;
        Equip(_gun);
    }

    private void Update()
    {
        if (_stats.IsDead || !CursorManager.Instance.IsLockCursor || GameManager.Instance.AutoMode) return;

        if (Input.GetMouseButtonDown(1))
        {
            ToggleZoomMode();
        }
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
        if (_gun.TryFire())
        {
            _animator.PlayFireAnimation();
        }
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

    private void ToggleZoomMode()
    {
        _zoomMode = _zoomMode switch
        {
            EZoomMode.Normal => EZoomMode.ZoomIn,
            _ => EZoomMode.Normal
        };
        OnZoomModeChanged?.Invoke(_zoomMode);
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
