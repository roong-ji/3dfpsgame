using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private EMonsterState _state = EMonsterState.Idle;

    private GameObject _player;
    private CharacterController _controller;

    private float _health = 100f;
    private float _damage = 10f;

    private float _attackDistance = 2f;
    private float _detectDistance = 10f;
    private float _moveSpeed = 5f;

    private float _nextAttackTime = 0f;
    private float _attackSpeed = 2f;

    private const float HitStunTime = 0.2f;
    private const float DeathDelayTime = 2f;

    private WaitForSeconds _hitWait = new WaitForSeconds(HitStunTime);
    private WaitForSeconds _deathWait = new WaitForSeconds(DeathDelayTime);

    private float _distance;

    private void Start()
    {
        _player = PlayerStats.Instance.gameObject;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, _player.transform.position);
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
        }
    }

    private void Idle()
    {
        // Todo: Idle 애니메이션 실행

        if (_distance <= _detectDistance)
        {
            _state = EMonsterState.Trace;
            Debug.Log("플레이어 추적");
        }
    }

    private void Trace()
    {
        // Todo: Run 애니메이션 실행

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _controller.Move(direction * _moveSpeed * Time.deltaTime);

        if(_distance > _detectDistance)
        {
            _state = EMonsterState.Idle;
            Debug.Log("정지");
        }

        if (_distance <= _attackDistance)
        {
            _state = EMonsterState.Attack;
            Debug.Log("플레이어 공격");
        }
    }

    private void Comeback()
    {

    }

    private void Attack()
    {
        if (Time.time >= _nextAttackTime)
        {
            // Todo: Attack 애니메이션 실행
            _nextAttackTime = Time.time + _attackSpeed;
            PlayerStats.Instance.TakeDamage(_damage);
        }

        if (_distance > _attackDistance)
        {
            _state = EMonsterState.Trace;
            Debug.Log("플레이어 추적");
        }
    }

    public bool TryTakeDamage(float damage)
    {
        if (_state == EMonsterState.Hit || _state == EMonsterState.Die) return false;
        _health -= damage;

        if( _health > 0 )
        {
            _state = EMonsterState.Hit;
            StartCoroutine(HitRoutine());
        }
        else
        {
            _state = EMonsterState.Die;
            StartCoroutine(DieRoutine());
        }

        return true;
    }

    private IEnumerator HitRoutine()
    {
        // Todo: Hit 애니메이션 실행
        Debug.Log("피격");

        yield return _hitWait;
        _state = EMonsterState.Idle;
    }

    private IEnumerator DieRoutine()
    {
        // Todo: Die 애니메이션 실행
        Debug.Log("사망");

        yield return _deathWait;
        Destroy(gameObject);
    }
}
