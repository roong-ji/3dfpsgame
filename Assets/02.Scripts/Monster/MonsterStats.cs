using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [SerializeField] private MonsterData _data;

    private ConsumableStat _health = new();

    private ValueStat _damage = new();
    private ValueStat _moveSpeed = new();
    private ValueStat _attackSpeed = new();

    private ValueStat _attackRange = new();
    private ValueStat _attackDistance = new();
    private ValueStat _detectDistance = new();
    private ValueStat _patrolDistance = new();
    private ValueStat _hitStunTime = new();

    public IConsumableStat Health => _health; 

    public IValueStat Damage => _damage;
    public IValueStat MoveSpeed => _moveSpeed;
    public IValueStat AttackSpeed => _attackSpeed;

    public IValueStat AttackRange => _attackRange;
    public IValueStat AttackDistance => _attackDistance;
    public IValueStat DetectDistance => _detectDistance;
    public IValueStat PatrolDistance => _patrolDistance;
    public IValueStat HitStunTime => _hitStunTime;

    private void Awake()
    {
        if (_data == null) return;
        Initialize(_data);
    }

    public void Initialize(MonsterData data)
    {
        _data = data;

        _health.SetMaxValue(_data.MaxHealth);
        _health.Initialize();

        _damage.SetValue(_data.Damage);
        _moveSpeed.SetValue(_data.MoveSpeed);
        _attackSpeed.SetValue(_data.AttackSpeed);

        _attackRange.SetValue(_data.AttackRange);
        _attackDistance.SetValue(_data.AttackDistance);
        _detectDistance.SetValue(_data.DetectDistance);
        _patrolDistance.SetValue(_data.PatrolDistance);
        _hitStunTime.SetValue(_data.HitStunTime);
    }

    public void ApplyDamage(float amount)
    {
        _health.Decrease(amount);
    }

    public bool IsDead => _health.Value <= 0;
}
