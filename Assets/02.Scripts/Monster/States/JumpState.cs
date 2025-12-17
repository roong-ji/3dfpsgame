using UnityEngine;

public class JumpState : BaseState
{
    private float _currentTime;
    private float _duration = 0.8f;
    private float _jumpHeight = 2.0f;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _endPositionOffsetY = 1.1f;

    public JumpState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        Debug.Log("상태 진입 : Jump");
        _currentTime = 0f;

        var data = _monster.CurrentJumpData;
        _startPosition = data.startPosition;
        _endPosition = data.endPosition;
        _endPosition.y += _endPositionOffsetY;

        _monster.StopAgent();
    }

    public override void OnStateExit() 
    {
        _monster.RestartAgent();
    }

    public override void OnStateUpdate()
    {
        if (_currentTime < _duration)
        {
            _currentTime += Time.deltaTime;
            float timeRate = _currentTime / _duration;

            Vector3 position = Vector3.Lerp(_startPosition, _endPosition, timeRate);
            position.y += _jumpHeight * Mathf.Sin(timeRate * Mathf.PI);

            _monster.transform.position = position;

            Vector3 direction = (_endPosition - _startPosition).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                _monster.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            _monster.transform.position = _endPosition;
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
