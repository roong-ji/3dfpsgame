using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    private Dictionary<EMonsterState, BaseState> _states = new();
    private BaseState _state;

    private GameObject _player;
    private CharacterController _controller;

    private float _health = 100f;
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

        ChangeState(EMonsterState.Idle);
    }

    private void Update()
    {
        _state.OnStateUpdate();
    }

    public void ChangeState(EMonsterState nextState)
    {
        _state = _states[nextState];
        _state.OnStateEnter();
    }

    public void Move(Vector3 direction)
    {
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_health <= 0) return false;
        _health -= damage.Amount;

        _lastDamageInfo = damage;

        if ( _health > 0 )
        {
            ChangeState(EMonsterState.Hit);
        }
        else
        {
            ChangeState(EMonsterState.Die);
        }

        return true;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
