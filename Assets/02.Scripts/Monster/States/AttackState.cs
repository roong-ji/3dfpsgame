using UnityEngine;

public class AttackState : BaseState
{
    private float _nextAttackTime = 0f;

    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        Debug.Log("상태 진입: Attack");
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (Time.time >= _nextAttackTime)
        {
            // Todo: Attack 애니메이션 실행
            _nextAttackTime = _monster.NextAttackTime;
            _monster.Attack();
        }

        if (_monster.Distance > _monster.AttackDistance)
        {
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
