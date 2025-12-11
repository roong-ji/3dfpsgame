using UnityEngine;

public class Monster : MonoBehaviour
{
    private EMonsterState _state = EMonsterState.Idle;

    [SerializeField] private GameObject _player;

    private void Update()
    {
        ActionByState();
    }

    private void ActionByState()
    {
        switch (_state)
        {
            case EMonsterState.Idle:
                Idle();
                break;
            case EMonsterState.Trace:
                Trace();
                break;
            case EMonsterState.Comeback:
                Comeback();
                break;
            case EMonsterState.Attack:
                Attack();
                break;
            case EMonsterState.Hit:
                Hit();
                break;
            case EMonsterState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        // Todo: Idle 애니메이션 실행
    }
    
    private void Trace()
    {
        // Todo: Run 애니메이션 실행
    }

    private void Comeback()
    {

    }

    private void Attack()
    {
        // Todo: Attack 애니메이션 실행

    }

    private void Hit()
    {
        // Todo: Hit 애니메이션 실행

    }

    private void Die()
    {
        // Todo: Die 애니메이션 실행

    }
}
