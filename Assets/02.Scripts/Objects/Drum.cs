using UnityEngine;

public class Drum : MonoBehaviour, IDamagable
{
    [SerializeField] private ValueStat _health;

    private ParticleSystem _explosionEffect;
    private Rigidbody _rigidbody;

    private Damage _damage;
    private float _spinForce = 5f;
    private float _throwForce = 10f;

    [SerializeField] private float _damageAmount = 50f;
    [SerializeField] private float _knockbackPower = 10f;

    [Header("폭발 범위")]
    [SerializeField] private float _explosionDistance = 10f;

    private void Start()
    {
        _explosionEffect = EffectManager.Instance.DrumExplosionEffect;
        _rigidbody = GetComponent<Rigidbody>();
        _damage.Amount = _damageAmount;
        _damage.KnockbackPower = _knockbackPower;
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_health.Value <= 0) return false;
        _health.Decrease(damage.Amount);

        if(_health.Value <= 0)
        {
            Explosion();
        }

        return true;
    }

    public void Explosion()
    {
        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            _explosionDistance
        );

        foreach (Collider target in targets)
        {
            if (target.gameObject == gameObject) continue;

            if (!target.TryGetComponent<IDamagable>(out IDamagable targetObject)) continue;
            targetObject.TryTakeDamage(_damage);
        }

        Vector3 flyDirection = (Vector3.up + Random.insideUnitSphere).normalized;
        Vector3 flySpin = Random.insideUnitSphere;
        flySpin.y = 0;

        _rigidbody.AddForce(flyDirection * _throwForce, ForceMode.Impulse);
        _rigidbody.AddTorque(flySpin * _spinForce, ForceMode.Impulse);

        _explosionEffect.transform.position = transform.position;
        _explosionEffect.Emit(1);
    }
}
