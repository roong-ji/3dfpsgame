using UnityEngine;

public class JumpState : BaseState
{
    private float _currentTime;
    private float _duration = 0.8f;
    private float _jumpHeight = 2.0f;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _endPosYOffset = 1.1f;

    public JumpState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        Debug.Log("상태 진입 : Jump");
        _currentTime = 0f;

        var data = _monster.CurrentJumpData;
        _startPos = data.startPos;
        _endPos = data.endPos;
        _endPos.y += _endPosYOffset;

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
            float t = _currentTime / _duration;

            Vector3 currentPos = Vector3.Lerp(_startPos, _endPos, t);
            currentPos.y += _jumpHeight * Mathf.Sin(t * Mathf.PI);

            _monster.transform.position = currentPos;

            Vector3 direction = (_endPos - _startPos).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                _monster.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            _monster.transform.position = _endPos;
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
