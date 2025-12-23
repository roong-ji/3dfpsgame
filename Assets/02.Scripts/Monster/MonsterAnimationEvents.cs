using UnityEngine;

public class MonsterAnimationEvents : MonoBehaviour
{
    protected Monster _monster;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
    }

    private void ApplyAttackDamage()
    {
        _monster.Attack();
    }

    private void ApplyMonsterDeath()
    {
        _monster.Death();
    }

    private void ApplyEndOfAnimation()
    {
        if (_monster.IsDead) return;
        _monster.RestartAgent();
        _monster.ChangeState(EMonsterState.Idle);
    }
}
