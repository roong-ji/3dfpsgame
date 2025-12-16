using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private ConsumableStat _health;
    [SerializeField] private ConsumableStat _stamina;

    public CountStat BombCount;
    public CountStat TotalBulletCount;

    public ValueStat Damage;
    public ValueStat MoveSpeed;
    public ValueStat RunSpeed;
    public ValueStat JumpPower;
    public ValueStat FireRate;

    public IConsumableStat Health => _health;
    public IConsumableStat Stamina => _stamina;

    private void Start()
    {
        _health.Initialize();
        _stamina.Initialize();
        BombCount.Initialize();
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
