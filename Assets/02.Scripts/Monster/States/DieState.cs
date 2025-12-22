using UnityEngine;

public class DieState : BaseState
{
    private static readonly int s_die = Animator.StringToHash("Die");

    public DieState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        EffectManager.Instance.StopBloodEffect();
        ItemDropper.DropRandomGolds(_monster.DropItem);
        _monster.PlayAnimation(s_die);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate() { }
}
