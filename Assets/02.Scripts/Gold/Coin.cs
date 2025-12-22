using UnityEngine;

public class Coin : MonoBehaviour, IPoolable
{
    [Header("드랍 연출 설정")]
    [SerializeField] private float _force;
    [SerializeField] private float _range;
    [SerializeField] private float _upForce;

    private GameObject _prefab;
    private Rigidbody _rigidbody;

    private bool _shouldRelease = false;

    public void Initialize(GameObject prefab)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _prefab = prefab;
        _shouldRelease = true;
    }

    private void OnEnable()
    {
        if (!_shouldRelease) return;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Scatter();
    }

    public void Scatter()
    {
        _rigidbody.AddExplosionForce(_force, Random.insideUnitSphere * _range, _range, _upForce, ForceMode.Impulse);
        _rigidbody.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
