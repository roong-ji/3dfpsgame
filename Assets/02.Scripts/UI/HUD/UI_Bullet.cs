using UnityEngine;
using UnityEngine.UI;

public class UI_Bullet : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    [Header("총알 UI")]
    [SerializeField] private Text _bulletCountTexTUI;

    private IResource _currentBullet;

    private void Awake()
    {
        PlayerGunController.AddListener(Initialize);
    }

    private void OnDestroy()
    {
        PlayerGunController.RemoveListener(Initialize);
        if (_currentBullet == null) return;
        _currentBullet.RemoveListener(UpdateBulletCountUI);
    }

    public void Initialize(NorseGun gun)
    {
        if (_currentBullet != null)
        {
            _currentBullet.RemoveListener(UpdateBulletCountUI);
        }
        _currentBullet = gun.Magazine.Bullet;
        _currentBullet.AddListener(UpdateBulletCountUI);
    }

    private void UpdateBulletCountUI(int _)
    {
        int maxBulletCount = _playerStats.Bullet.Count;
        int bulletCount = _currentBullet.Count;

        _bulletCountTexTUI.text = $"{bulletCount}/{maxBulletCount}";
    }
}
