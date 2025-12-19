using UnityEngine;

public class ComebackState : BaseState
{
    private Vector3 _originPosition;
    private const float Epsilon = 0.1f;

    public ComebackState(Monster monster) : base(monster) 
    {
        _originPosition = monster.gameObject.transform.position;
    }

    public override void OnStateEnter()
    {
        _monster.MoveToPosition(_originPosition);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        Vector3 position = _monster.gameObject.transform.position;

        if ((position - _originPosition).sqrMagnitude <= Epsilon)
        {
            _monster.ChangeState(EMonsterState.Idle);
        }

        if (_monster.Distance <= _monster.DetectDistance)
        {
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
