using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public ConsumableStat Health;
    public ConsumableStat Stamina;

    public CountStat BombCount;
    public CountStat TotalBulletCount;

    public ValueStat Damage;
    public ValueStat MoveSpeed;
    public ValueStat RunSpeed;
    public ValueStat JumpPower;
    public ValueStat FireRate;

    private void Start()
    {
        Health.Initialize();
        Stamina.Initialize();
        BombCount.Initialize();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        Health.Regenerate(deltaTime);
        Stamina.Regenerate(deltaTime);
    }

    public void TakeDamage(float damage)
    {
        Health.Decrease(damage);

        if(Health.Value <= 0)
        {
            // Todo: 사망 로직 작성
        }
    }
}
