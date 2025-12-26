using UnityEngine;

public class EliteTraceState : BaseState
{
    private static readonly int s_traceToAttackIdle = Animator.StringToHash("TraceToAttackIdle");
    private static readonly int s_traceToJump = Animator.StringToHash("TraceToJump");
    private static readonly int s_traceToPatrol = Animator.StringToHash("TraceToPatrol");

    public EliteTraceState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _monster.SetStoppintDistance(_monster.AttackDistance);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (_monster.CheckOffMeshLink())
        {
            _monster.PlayAnimation(s_traceToJump);
            _monster.ChangeState(EMonsterState.Jump);
            return;
        }

        _monster.MoveToPosition(_monster.TargetPosition);

        float distance = _monster.Distance;

        if (distance > _monster.DetectDistance)
        {
            _monster.PlayAnimation(s_traceToPatrol);
            _monster.ChangeState(EMonsterState.Patrol);
        }

        if (distance <= _monster.AttackDistance)
        {
            _monster.PlayAnimation(s_traceToAttackIdle);
            _monster.ChangeState(EMonsterState.Attack);
        }
    }
}
