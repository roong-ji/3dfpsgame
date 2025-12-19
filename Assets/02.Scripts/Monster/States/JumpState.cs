using UnityEngine;

public class JumpState : BaseState
{
    private static readonly int s_jumpToTrace = Animator.StringToHash("JumpToTrace");

    private float _currentTime;
    private float _duration = 0.8f;
    private float _jumpHeight = 2.0f;
    private Line _jump;
    private float _endPositionOffsetY = 1.1f;

    public JumpState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _currentTime = 0f;

        _jump = _monster.CurrentJumpData;
        _jump.End.y += _endPositionOffsetY;

        _monster.StopAgent();
    }

    public override void OnStateExit() 
    {
        _monster.PlayAnimation(s_jumpToTrace);
        _monster.RestartAgent();
    }

    public override void OnStateUpdate()
    {
        if (_currentTime < _duration)
        {
            _currentTime += Time.deltaTime;
            float timeRate = _currentTime / _duration;

            Vector3 position = Vector3.Lerp(_jump.Start, _jump.End, timeRate);
            position.y += _jumpHeight * Mathf.Sin(timeRate * Mathf.PI);

            _monster.transform.position = position;
        }
        else
        {
            _monster.ChangeState(EMonsterState.Trace);
        }
    }
}
