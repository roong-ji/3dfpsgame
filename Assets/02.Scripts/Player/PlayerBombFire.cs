using UnityEngine;

public class PlayerBombFire : MonoBehaviour
{
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _throwPower = 15f;

    private Transform _mainCameraTransform;

    private ICountStat _bombCount;

    private PlayerAnimator _animator;

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
        _bombCount = GetComponent<PlayerStats>().BombCount;
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q) || GameManager.Instance.AutoMode) return;
        _animator.PlayThrowAnimation();
    }

    public void BombFire()
    {
        if (!_bombCount.TryConsume(1)) return;

        GameObject bombObject = PoolManager.Instance.Spawn(_bombPrefab, _fireTransform.position);
        
        Bomb bomb = bombObject.GetComponent<Bomb>();
        bomb.SetAttacker(gameObject);
        bomb.Throw(_mainCameraTransform.forward * _throwPower);
    }
}
