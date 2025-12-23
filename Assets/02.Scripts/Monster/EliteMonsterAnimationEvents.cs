using UnityEngine;

public class EliteMonsterAnimationEvents : MonsterAnimationEvents
{
    private void ChargeToTrace()
    {
        _monster.ChangeState(EMonsterState.Trace);
    }
}
