using UnityEngine;

public class MonsterAnimationEvents : MonoBehaviour
{
    private Monster _monster;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
    }

    private void ApplyAttackDamage()
    {
        _monster.Attack();
    }

    private void ApplyMonsterDeath()
    {
        _monster.Death();
    }

    private void ApplyEndOfAnimation()
    {
        _monster.RestartAgent();
        _monster.ChangeState(EMonsterState.Idle);
    }
}
