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

        GameObject bomb = PoolManager.Instance.Spawn(_bombPrefab, _fireTransform.position);
        Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
        rigidbody.AddForce(_mainCameraTransform.forward * _throwPower, ForceMode.Impulse);
    }
}
