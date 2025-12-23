using UnityEngine;

public class EliteMonsterAnimationEvents : MonoBehaviour
{
    private EliteMonster _monster;

    private void Awake()
    {
        _monster = GetComponentInParent<EliteMonster>();
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

    private void ChargeToTrace()
    {
        _monster.ChangeState(EMonsterState.Trace);
    }
}
