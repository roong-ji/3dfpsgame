using UnityEngine;

public class IdleState : BaseState
{
    private static readonly int s_idleToTrace = Animator.StringToHash("IdleToTrace");

    public IdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        _monster.PlayAnimation(s_idleToTrace);

        if (_monster.Distance <= _monster.DetectDistance)
        {
            _monster.ChangeState(EMonsterState.Trace);
        }

        else
        {
            _monster.ChangeState(EMonsterState.Patrol);
        }
    }
}
