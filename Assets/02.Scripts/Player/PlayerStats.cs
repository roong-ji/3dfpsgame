using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private ConsumableStat _health;
    [SerializeField] private ConsumableStat _stamina;

    [SerializeField] private CountStat _bombCount;
    [SerializeField] private CountStat _totalBulletCount;

    [SerializeField] private ValueStat _moveSpeed;
    [SerializeField] private ValueStat _runSpeed;
    [SerializeField] private ValueStat _jumpPower;

    public IConsumableStat Health => _health;
    public IConsumableStat Stamina => _stamina;

    public ICountStat BombCount => _bombCount;
    public ICountStat TotalBulletCount => _totalBulletCount;

    public IValueStat MoveSpeed => _moveSpeed;
    public IValueStat RunSpeed => _runSpeed;
    public IValueStat JumpPower => _jumpPower;

    private void Start()
    {
        _health.Initialize();
        _stamina.Initialize();
        _bombCount.Initialize();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        _health.Regenerate(deltaTime);
        _stamina.Regenerate(deltaTime);
    }

    public void ApplyDamage(float damage)
    {
        _health.Decrease(damage);
    }

    public bool IsDead => _health.Value <= 0;
}
