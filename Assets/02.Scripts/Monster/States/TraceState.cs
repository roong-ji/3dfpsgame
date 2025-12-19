using UnityEngine;

public class TraceState : BaseState
{
    private static readonly int s_traceToAttackIdle = Animator.StringToHash("TraceToAttackIdle");

    public TraceState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _monster.SetStoppintDistance(_monster.AttackDistance);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (_monster.CheckOffMeshLink())
        {
            _monster.ChangeState(EMonsterState.Jump);
            return;
        }

        _monster.MoveToPosition(_monster.TargetPosition);

        float distance = _monster.Distance;

        if (distance > _monster.DetectDistance)
        {
            _monster.ChangeState(EMonsterState.Comeback);
        }

        if (distance <= _monster.AttackDistance)
        {
            _monster.PlayAnimation(s_traceToAttackIdle);
            _monster.ChangeState(EMonsterState.Attack);
        }
    }
}
