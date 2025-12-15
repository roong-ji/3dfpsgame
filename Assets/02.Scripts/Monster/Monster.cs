using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    private Dictionary<EMonsterState, BaseState> _states;
    private BaseState _state;

    [SerializeField] private GameObject _player;
    private IDamagable _attackTarget;
    private CharacterController _controller;

    public ConsumableStat Health;
    private float _damage = 10f;

    private float _attackDistance = 2f;
    private float _detectDistance = 10f;
    private float _patrolDistance = 5f;

    private float _moveSpeed = 3f;
    private float _attackSpeed = 2f;

    private float _hitStunTime = 0.2f;
    private float _deathDelayTime = 2f;

    public float Damage => _damage;
    public float AttackDistance => _attackDistance;
    public float DetectDistance => _detectDistance;
    public float PatrolDistance => _patrolDistance;
    public float NextAttackTime => Time.time + _attackSpeed;

    public float HitTime => _hitStunTime;
    public float DeathTime => _deathDelayTime;

    private Damage _lastDamageInfo;
    public Damage LastDamageInfo => _lastDamageInfo;

    public float Distance => Vector3.Distance(transform.position, _player.transform.position);

    public Vector3 DirectionToPlayer => (_player.transform.position - transform.position).normalized;

    public bool IsDead => Health.Value <= 0;

    // Todo: MonsterStats 분리

    public event Action OnTakeDamaged;

    private void Start()
    {
        Health.Initialize();

        _attackTarget = _player.GetComponent<IDamagable>();
        _controller = GetComponent<CharacterController>();

        _states = new Dictionary<EMonsterState, BaseState>
        {
            { EMonsterState.Idle, new IdleState(this) },
            { EMonsterState.Patrol, new PatrolState(this) },
            { EMonsterState.Trace, new TraceState(this) },
            { EMonsterState.Comeback, new ComebackState(this) },
            { EMonsterState.Attack, new AttackState(this) },
            { EMonsterState.Hit, new HitState(this) },
            { EMonsterState.Die, new DieState(this) }
        };

        ChangeState(EMonsterState.Idle);
    }

    private void Update()
    {
        _state.OnStateUpdate();
    }

    public void ChangeState(EMonsterState nextState)
    {
        if(_state != null)
        {
            _state.OnStateExit();
        }

        _state = _states[nextState];
        _state.OnStateEnter();
    }

    public void Move(Vector3 direction)
    {
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (Health.Value <= 0) return false;
        Health.Decrease(damage.Amount);

        _lastDamageInfo = damage;
        OnTakeDamaged?.Invoke();

        if ( Health.Value > 0 )
        {
            ChangeState(EMonsterState.Hit);
        }
        else
        {
            ChangeState(EMonsterState.Die);
        }

        return true;
    }

    public void Attack()
    {
        _attackTarget.TryTakeDamage(new Damage(
                _damage,
                transform.position,
                transform.position,
                gameObject
            ));
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
