using UnityEngine;

public class HitState : BaseState
{
    private float _timer;
    private float _knockbackSpeed;
    private Vector3 _knockbackDirection;

    public HitState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        // Todo: Hit 애니메이션 실행

        _timer = 0;
        
        var damage = _monster.LastDamageInfo;

        _knockbackSpeed = damage.KnockbackPower;
        _knockbackDirection = damage.AttackerPoint - _monster.gameObject.transform.position;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }
}
