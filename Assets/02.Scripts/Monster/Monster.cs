using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private EMonsterState _state = EMonsterState.Idle;

    private GameObject _player;
    private CharacterController _controller;

    private float _health = 100f;
    private float _damage = 10f;

    private float _attackDistance = 2f;
    private float _detectDistance = 5f;
    private float _patrolDistance = 5f;
    private float _moveSpeed = 3f;
    private Vector3 _patrolPoint = Vector3.zero;

    private const float Epsilon = 0.1f;

    private float _nextAttackTime = 0f;
    private float _attackSpeed = 2f;

    private float _knockbackSpeed = -2f;
    private float _hitStunTime = 0.2f;
    private float _deathDelayTime = 2f;
    private float _deathYPosition = 0.5f;
    private Vector3 _deathRotation = new Vector3(90f, 0, 0);

    private Vector3 _originPosition;
    private float _distance;

    private void Start()
    {
        _player = PlayerStats.Instance.gameObject;
        _controller = GetComponent<CharacterController>();
        _originPosition = transform.position;
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
            case EMonsterState.Patrol:
                Patrol();
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

        else
        {
            _state = EMonsterState.Patrol;
            Debug.Log("순찰 시작");
        }
    }

    private void Patrol()
    {
        if(_patrolPoint == Vector3.zero || (_patrolPoint - transform.position).sqrMagnitude <= Epsilon)
        {
            Debug.Log("다음 순찰 지점");
            _patrolPoint = Random.insideUnitSphere * _patrolDistance;
            _patrolPoint.y = _originPosition.y;
        }

        Vector3 direction = (_patrolPoint - transform.position).normalized;

        _controller.Move(direction * _moveSpeed * Time.deltaTime);

        if (_distance <= _detectDistance)
        {
            _patrolPoint = Vector3.zero;
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
            _state = EMonsterState.Comeback;
            Debug.Log("돌아가기");
        }

        if (_distance <= _attackDistance)
        {
            _state = EMonsterState.Attack;
            Debug.Log("플레이어 공격");
        }
    }

    private void Comeback()
    {
        Vector3 direction = (_originPosition - transform.position).normalized;
        _controller.Move(direction * _moveSpeed * Time.deltaTime);

        if ((transform.position - _originPosition).sqrMagnitude <= Epsilon)
        {
            _state = EMonsterState.Idle;
            Debug.Log("정지");
        }

        if (_distance <= _detectDistance)
        {
            _state = EMonsterState.Trace;
            Debug.Log("추적");
        }
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

        float currentSpeed = _knockbackSpeed;
        Vector3 hitDirection = _player.transform.position - transform.position;

        float timer = 0f;
        while(timer < _hitStunTime)
        {
            timer += Time.deltaTime;

            _controller.Move(hitDirection * currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, timer / _hitStunTime);

            yield return null;
        }

        _state = EMonsterState.Idle;
    }

    private IEnumerator DieRoutine()
    {
        // Todo: Die 애니메이션 실행

        Quaternion targetQuaternion = Quaternion.Euler(_deathRotation);
        Vector3 targetPosition = new Vector3(transform.position.x, _deathYPosition, transform.position.z);

        float timer = 0f;
        while (timer < _deathDelayTime)
        {
            timer += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, timer / _deathDelayTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, timer / _deathDelayTime);

            yield return null;
        }

        yield return null;
        Destroy(gameObject);
    }
}
