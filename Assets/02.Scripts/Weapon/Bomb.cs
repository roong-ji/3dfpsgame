using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable
{
    private GameObject _prefab;
    private ParticleSystem _explosionEffect;

    private Rigidbody _rigidbody;

    private Damage _damage;

    // Todo : SO 방식 도입
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _knockbackPower;

    [Header("폭발 범위")]
    [SerializeField] private float _explosionDistance;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        _explosionEffect = EffectManager.Instance.BombExplosionEffect;
        _rigidbody = GetComponent<Rigidbody>();
        _damage.Amount = _damageAmount;
        _damage.KnockbackPower = _knockbackPower;
    }

    public void SetAttacker(GameObject attacker)
    {
        _damage.Attacker = attacker;
    }

    public void Throw(Vector3 throwForce)
    {
        _damage.AttackerPoint = transform.position;
        _rigidbody.AddForce(throwForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    private void Explosion()
    {
        _damage.HitPoint = transform.position;

        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            _explosionDistance
        );

        foreach (Collider target in targets)
        {
            if (!target.TryGetComponent<IDamagable>(out IDamagable targetObject)) continue;
            targetObject.TryTakeDamage(_damage);
        }

        _explosionEffect.transform.position = transform.position;
        _explosionEffect.Play();
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
