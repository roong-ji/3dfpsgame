using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        // Todo: Idle 애니메이션 실행
        Debug.Log("상태 진입 : Idle");
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
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
