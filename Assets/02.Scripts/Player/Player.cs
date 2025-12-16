using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private PlayerStats _stats;

    public event Action OnTakeDamaged;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_stats.IsDead) return false;

        _stats.ApplyDamage(damage.Amount);
        OnTakeDamaged?.Invoke();

        if (_stats.IsDead)
        {
            GameManager.Instance.GameOver();
        }

        return true;
    }
}
