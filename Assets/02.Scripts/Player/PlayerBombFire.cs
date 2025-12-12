using UnityEngine;

public class PlayerBombFire : MonoBehaviour
{
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _throwPower = 15f;

    private Transform _mainCameraTransform;

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        BombFire();
    }

    private void BombFire()
    {
        if (!PlayerStats.Instance.BombCount.TryConsume(1)) return;

        GameObject bombObject = PoolManager.Instance.Spawn(_bombPrefab, _fireTransform.position);
        
        Bomb bomb = bombObject.GetComponent<Bomb>();
        bomb.SetAttacker(gameObject);
        bomb.Throw(_mainCameraTransform.forward * _throwPower);
    }
}
