using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public ConsumableStat Health;
    public ConsumableStat Stamina;
    public ItemCount BombCount;

    public ValueStat Damage;
    public ValueStat MoveSpeed;
    public ValueStat RunSpeed;
    public ValueStat JumpPower;

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
}
