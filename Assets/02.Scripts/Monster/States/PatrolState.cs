using UnityEngine;

public class PatrolState : BaseState
{
    private Vector3 _patrolPoint;
    private const float Epsilon = 1.5f;

    public PatrolState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _monster.SetStoppintDistance(0);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        Vector3 position = _monster.gameObject.transform.position;

        if (_patrolPoint == Vector3.zero || (_patrolPoint - position).sqrMagnitude <= Epsilon)
        {
            _patrolPoint = position + Random.insideUnitSphere * _monster.PatrolDistance;
            _patrolPoint.y = position.y;
        }

        _monster.MoveToPosition(_patrolPoint);

        if (_monster.Distance <= _monster.DetectDistance)
        {
            _patrolPoint = Vector3.zero;
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
