using UnityEngine;

public class ChargeState : BaseState
{
    public ChargeState(Monster monster) : base(monster) { }

    public override void OnStateEnter() { Debug.Log("상태 진입 : Charge"); }

    public override void OnStateExit() { }

    public override void OnStateUpdate() { }
}
