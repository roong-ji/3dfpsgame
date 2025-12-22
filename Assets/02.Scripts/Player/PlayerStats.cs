using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private ConsumableStat _health;
    [SerializeField] private ConsumableStat _stamina;

    [SerializeField] private Resource _bomb;
    [SerializeField] private Resource _bullet;

    [SerializeField] private ValueStat _moveSpeed;
    [SerializeField] private ValueStat _runSpeed;
    [SerializeField] private ValueStat _jumpPower;

    public IConsumableStat Health => _health;
    public IConsumableStat Stamina => _stamina;

    public IResource Bomb => _bomb;
    public IResource Bullet => _bullet;

    public IValueStat MoveSpeed => _moveSpeed;
    public IValueStat RunSpeed => _runSpeed;
    public IValueStat JumpPower => _jumpPower;

    private void Start()
    {
        _health.Initialize();
        _stamina.Initialize();
        _bomb.Initialize();
    }

    private void Update()
    {
        if (IsDead) return;

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
