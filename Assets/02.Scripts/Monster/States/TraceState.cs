using UnityEngine;

public class TraceState : BaseState
{
    private readonly string _idleToTrace = "IdleToTrace";

    public TraceState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _monster.PlayAnimation(_idleToTrace);
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
            _monster.ChangeState(EMonsterState.Attack);
        }
    }
}
