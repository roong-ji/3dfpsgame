using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamagable
{
    protected Dictionary<EMonsterState, BaseState> _states;
    private BaseState _state;

    [SerializeField] private GameObject _player;
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Animator _animator;

    private MonsterStats _stats;
    private MonsterAttack _attack;

    public IConsumableStat Health => _stats.Health;

    public float AttackDistance => _stats.AttackDistance.Value;
    public float DetectDistance => _stats.DetectDistance.Value;
    public float PatrolDistance => _stats.PatrolDistance.Value;
    public float NextAttackTime => Time.time + _stats.AttackSpeed.Value;

    public float HitTime => _stats.HitStunTime.Value;

    public ItemData DropItem => _stats.DropItem;

    private Damage _lastDamageInfo;
    public Damage LastDamageInfo => _lastDamageInfo;

    public float Distance => Vector3.Distance(transform.position, _player.transform.position);

    public Vector3 TargetPosition => _player.transform.position;

    private Line _jumpData;
    public Line CurrentJumpData => _jumpData;

    public bool IsDead => _stats.IsDead;

    public event Action OnTakeDamaged;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<MonsterStats>();
        _attack = GetComponent<MonsterAttack>();
        _attack.Initialize(_stats);

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.MoveSpeed.Value;

        Initialize();
    }

    protected virtual void Initialize()
    {
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
        _controller.Move(direction * _stats.MoveSpeed.Value * Time.deltaTime);
    }

    public void SetStoppintDistance(float distance)
    {
        _agent.stoppingDistance = distance;
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public bool CheckOffMeshLink()
    {
        if (!_agent.isOnOffMeshLink) return false;

        OffMeshLinkData data = _agent.currentOffMeshLinkData;
        _jumpData = new Line
        {
            Start = transform.position,
            End = data.endPos
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

    public void PlayAnimation(int trigger)
    {
        _animator.SetTrigger(trigger);
    }

    public void PlayAnimation(int trigger, bool on)
    {
        _animator.SetBool(trigger, on);
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_state is DieState) return false;
        _stats.ApplyDamage(damage.Amount);

        _lastDamageInfo = damage;
        OnTakeDamaged?.Invoke();

        if (IsDead)
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
        _attack.Attack();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
