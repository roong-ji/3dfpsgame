using UnityEngine;

public class EliteMonsterAnimationEvents : MonsterAnimationEvents
{
    private void ChargeToTrace()
    {
        _monster.ChangeState(EMonsterState.Trace);
    }

    private void ApplyEndOfHitAnimation()
    {
        if (_monster.IsDead) return;
        _monster.RestartAgent();
        _monster.ChangeState(EMonsterState.Trace);
    }
}
