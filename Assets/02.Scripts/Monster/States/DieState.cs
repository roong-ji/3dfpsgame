using UnityEngine;

public class DieState : BaseState
{
    private float _timer;

    private float _deathYPosition = 0.5f;
    private Vector3 _deathRotation = new Vector3(90f, 0, 0);

    private Transform _monsterTransform;
    private Quaternion _targetQuaternion;
    private Vector3 _targetPosition;

    public DieState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        // Todo: Die 애니메이션 실행
        Debug.Log("상태 진입 : Die");

        _timer = 0;

        _targetQuaternion = Quaternion.Euler(_deathRotation);
        _targetPosition = _monster.gameObject.transform.position;
        _targetPosition.y = _deathYPosition;

        _monsterTransform = _monster.gameObject.transform;
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (_timer < _monster.DeathTime)
        {
            _timer += Time.deltaTime;

            _monsterTransform.rotation = Quaternion.Lerp(_monsterTransform.rotation, _targetQuaternion, _timer / _monster.DeathTime);
            _monsterTransform.position = Vector3.Lerp(_monsterTransform.position, _targetPosition, _timer / _monster.DeathTime);
        }
        else
        {
            _monster.Death();
        }
    }
}
