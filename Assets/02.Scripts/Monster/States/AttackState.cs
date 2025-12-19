using UnityEngine;

public class AttackState : BaseState
{
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_attackToTrace = Animator.StringToHash("AttackToTrace");

    private float _nextAttackTime = 0f;

    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        _monster.transform.LookAt(_monster.TargetPosition);

        if (Time.time >= _nextAttackTime)
        {
            _monster.PlayAnimation(s_attack);
            _nextAttackTime = _monster.NextAttackTime;
        }

        if (_monster.Distance > _monster.AttackDistance)
        {
            _monster.PlayAnimation(s_attackToTrace);
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
