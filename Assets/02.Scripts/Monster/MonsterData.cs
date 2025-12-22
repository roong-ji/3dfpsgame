using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "GameData/MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("기본 스탯")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackSpeed;

    [Header("행동 스탯")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _patrolDistance;
    [SerializeField] private float _hitStunTime;

    [Header("드랍 코인 개수")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _minCoin;
    [SerializeField] private int _maxCoin;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float AttackSpeed => _attackSpeed;

    public float AttackRange => _attackRange;
    public float AttackDistance => _attackDistance;
    public float DetectDistance => _detectDistance;
    public float PatrolDistance => _patrolDistance;
    public float HitStunTime => _hitStunTime;

    public GameObject CoinPrefab => _coinPrefab;
    public int MinCoin => _minCoin;
    public int MaxCoin => _maxCoin;
}
