using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _targetLayer;
    private IValueStat _attackRange;
    private IValueStat _damage;
    private Collider[] _hitBuffer = new Collider[1];

    public void Initialize(MonsterStats stats)
    {
        _attackRange = stats.AttackRange;
        _damage = stats.Damage;
    }

    public void Attack()
    {
        int count = Physics.OverlapSphereNonAlloc(_attackPoint.position, _attackRange.Value, _hitBuffer, _targetLayer);
        if (count <= 0) return;

        IDamagable targetObject = _hitBuffer[0].GetComponent<IDamagable>();
        targetObject.TryTakeDamage(new Damage(_damage.Value, _attackPoint.position, transform.position, gameObject));
    }
}
