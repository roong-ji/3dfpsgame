using UnityEngine;

public class HitState : BaseState
{
    private static readonly int s_hit = Animator.StringToHash("Hit");

    private float _timer;
    
    private float _knockbackSpeed;
    private Vector3 _knockbackDirection;

    public HitState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        _monster.PlayAnimation(s_hit);

        _timer = 0;
        
        var damage = _monster.LastDamageInfo;

        _knockbackSpeed = damage.KnockbackPower;
        _knockbackDirection = (_monster.gameObject.transform.position - damage.AttackerPoint).normalized;

        EffectManager.Instance.PlayBloodEffect(_monster.transform, damage.Normal);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate()
    {
        if (_timer < _monster.HitTime)
        {
            _timer += Time.deltaTime;

            _monster.Move(_knockbackDirection * _knockbackSpeed);
            _knockbackSpeed = Mathf.Lerp(_knockbackSpeed, 0f, _timer / _monster.HitTime);
        }
    }
}
