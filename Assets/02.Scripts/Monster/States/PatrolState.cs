using UnityEngine;

public class PatrolState : BaseState
{
    private Vector3 _patrolPoint;
    private const float Epsilon = 0.1f;

    public PatrolState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {        
        // Todo: Run 애니메이션 실행
        Debug.Log("상태 진입 : Patrol");
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        Vector3 position = _monster.gameObject.transform.position;

        if (_patrolPoint == Vector3.zero || (_patrolPoint - position).sqrMagnitude <= Epsilon)
        {
            Debug.Log("다음 순찰 지점");
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
