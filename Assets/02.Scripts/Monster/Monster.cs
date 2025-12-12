using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Dictionary<EMonsterState, BaseState> _states = new();
    private BaseState _state;
    private EMonsterState _stateEnum;

    private GameObject _player;
    private CharacterController _controller;

    private float _health = 100f;
    private float _damage = 10f;

    private float _attackDistance = 2f;
    private float _detectDistance = 5f;
    private float _patrolDistance = 5f;

    private float _moveSpeed = 3f;
    private float _attackSpeed = 2f;

    private float _hitStunTime = 0.2f;
    private float _deathDelayTime = 2f;
    private float _deathYPosition = 0.5f;
    private Vector3 _deathRotation = new Vector3(90f, 0, 0);

    public float Damage => _damage;
    public float AttackDistance => _attackDistance;
    public float DetectDistance => _detectDistance;
    public float PatrolDistance => _patrolDistance;
    public float NextAttackTime => Time.time + _attackSpeed;

    private Damage _lastDamageInfo;
    public Damage LastDamageInfo => _lastDamageInfo;

    public float Distance => Vector3.Distance(transform.position, _player.transform.position);

    public Vector3 DirectionToPlayer => (_player.transform.position - transform.position).normalized;

    private void Start()
    {
        _player = PlayerStats.Instance.gameObject;
        _controller = GetComponent<CharacterController>();

        _states.Add(EMonsterState.Idle, new IdleState(this));
        _states.Add(EMonsterState.Patrol, new PatrolState(this));
        _states.Add(EMonsterState.Trace, new TraceState(this));
        _states.Add(EMonsterState.Comeback, new ComebackState(this));
        _states.Add(EMonsterState.Attack, new AttackState(this));
        _states.Add(EMonsterState.Hit, new HitState(this));
        _states.Add(EMonsterState.Die, new DieState(this));

        _state = _states[EMonsterState.Idle];
    }

    private void Update()
    {
        _state.OnStateUpdate();
    }

    public void ChangeState(EMonsterState nextState)
    {
        _state.OnStateExit();
        _state = _states[nextState];
        _state.OnStateEnter();
    }

    public void Move(Vector3 direction)
    {
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_stateEnum == EMonsterState.Hit || _stateEnum == EMonsterState.Die) return false;
        _health -= damage.Amount;

        _lastDamageInfo = damage;

        if ( _health > 0 )
        {
            ChangeState(EMonsterState.Hit);
            StartCoroutine(HitRoutine(damage));
        }
        else
        {
            ChangeState(EMonsterState.Die);
            StartCoroutine(DieRoutine());
        }

        return true;
    }

    private IEnumerator HitRoutine(Damage damage)
    {
        // Todo: Hit 애니메이션 실행

        float currentSpeed = damage.KnockbackPower;
        Vector3 hitDirection = transform.position - damage.AttackerPoint;

        float timer = 0f;
        while (timer < _hitStunTime)
        {
            timer += Time.deltaTime;

            _controller.Move(hitDirection * currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, timer / _hitStunTime);

            yield return null;
        }

        _stateEnum = EMonsterState.Idle;
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
