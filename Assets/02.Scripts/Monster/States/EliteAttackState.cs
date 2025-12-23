using UnityEngine;

public class EliteAttackState : BaseState
{
    private static readonly int[] s_attackHashes =
    {
        Animator.StringToHash("PunchAttack"),
        Animator.StringToHash("ShortAttack"),
        Animator.StringToHash("SmashAttack"),
        Animator.StringToHash("HookAttack")
    };

    private static readonly int s_attackToTrace = Animator.StringToHash("AttackToTrace");

    private float _nextAttackTime = 0f;

    public EliteAttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter() { Debug.Log("상태 진입 : Attack"); }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (Time.time >= _nextAttackTime)
        {
            int randomIndex = Random.Range(0, s_attackHashes.Length);
            _monster.PlayAnimation(s_attackHashes[randomIndex]);
            _nextAttackTime = _monster.NextAttackTime;
        }

        if (_monster.Distance > _monster.AttackDistance)
        {
            _monster.PlayAnimation(s_attackToTrace);
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}