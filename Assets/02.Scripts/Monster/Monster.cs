using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamagable
{
    private Dictionary<EMonsterState, BaseState> _states;
    private BaseState _state;

    [SerializeField] private GameObject _player;
    private IDamagable _attackTarget;
    private CharacterController _controller;
    private NavMeshAgent _agent;

    public ConsumableStat Health;
    private float _damage = 10f;

    private float _attackDistance = 2f;
    private float _detectDistance = 15f;
    private float _patrolDistance = 15f;

    private float _moveSpeed = 3f;
    private float _attackSpeed = 2f;

    private float _hitStunTime = 0.2f;
    private float _deathDelayTime = 02f;

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

    public Vector3 TargetPosition => _player.transform.position;

    public bool IsDead => Health.Value <= 0;

    public struct JumpData
    {
        public Vector3 startPos;
        public Vector3 endPos;
    }
    private JumpData _jumpData;
    public JumpData CurrentJumpData => _jumpData;

    // Todo: MonsterStats 분리

    public event Action OnTakeDamaged;

    private void Start()
    {
        Health.Initialize();

        _attackTarget = _player.GetComponent<IDamagable>();
        _controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
        _agent.stoppingDistance = _attackDistance;

        _agent.autoTraverseOffMeshLink = false;

        _states = new Dictionary<EMonsterState, BaseState>
        {
            { EMonsterState.Idle, new IdleState(this) },
            { EMonsterState.Patrol, new PatrolState(this) },
            { EMonsterState.Trace, new TraceState(this) },
            { EMonsterState.Jump, new JumpState(this) },
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
        _state?.OnStateExit();
        _state = _states[nextState];
        _state.OnStateEnter();
    }

    public void Move(Vector3 direction)
    {
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public bool CheckOffMeshLink()
    {
        if (!_agent.isOnOffMeshLink) return false;

        OffMeshLinkData data = _agent.currentOffMeshLinkData;
        _jumpData = new JumpData
        {
            startPos = transform.position,
            endPos = data.endPos
        };
        return true;
    }

    public void StopAgent()
    {
        _agent.isStopped = true;
        _agent.CompleteOffMeshLink();
    }

    public void RestartAgent()
    {
        _agent.isStopped = false;
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (Health.Value <= 0) return false;
        Health.Decrease(damage.Amount);

        _lastDamageInfo = damage;
        OnTakeDamaged?.Invoke();

        if (Health.Value <= 0)
        {
            _agent.enabled = false;
            ChangeState(EMonsterState.Die);
        }
        else if (_state is not JumpState)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            ChangeState(EMonsterState.Hit);
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
