using UnityEngine;

public class Coin : MonoBehaviour, IPoolable
{
    [Header("드랍 연출 설정")]
    [SerializeField] private float _force = 20f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _upForce = 2f;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("충돌");
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
