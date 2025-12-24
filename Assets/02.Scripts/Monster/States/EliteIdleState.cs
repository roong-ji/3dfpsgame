using UnityEngine;

public class EliteIdleState : BaseState
{
    private static readonly int s_charge = Animator.StringToHash("Charge");
    private static readonly int s_patrol = Animator.StringToHash("Patrol");

    public EliteIdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {

        if (_monster.Distance <= _monster.DetectDistance)
        {
            _monster.PlayAnimation(s_charge);
            _monster.ChangeState(EMonsterState.Charge);
        }

        else
        {
            _monster.PlayAnimation(s_patrol);
            _monster.ChangeState(EMonsterState.Patrol);
        }
    }
}
