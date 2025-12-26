using System.Collections.Generic;

public class EliteMonster : Monster
{
    protected override void Initialize()
    {
        _states = new Dictionary<EMonsterState, BaseState>
        {
            { EMonsterState.Idle, new EliteIdleState(this) },
            { EMonsterState.Patrol, new ElitePatrolState(this) },
            { EMonsterState.Trace, new EliteTraceState(this) },
            { EMonsterState.Jump, new JumpState(this) },
            { EMonsterState.Charge, new ChargeState(this) },
            { EMonsterState.Attack, new EliteAttackState(this) },
            { EMonsterState.Hit, new HitState(this) },
            { EMonsterState.Die, new DieState(this) }
        };

        ChangeState(EMonsterState.Idle);
    }
}
