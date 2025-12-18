using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private PlayerStats _stats;
    private PlayerAnimator _animator;
    private PlayerBombFire _bombFire;

    public event Action OnTakeDamaged;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponent<PlayerAnimator>();
        _bombFire = GetComponent<PlayerBombFire>();
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_stats.IsDead) return false;

        _stats.ApplyDamage(damage.Amount);
        OnTakeDamaged?.Invoke();

        _animator.InjureAnimationRate(1f - _stats.Health.Value / _stats.Health.MaxValue);

        if (_stats.IsDead)
        {
            _animator.PlayDeathAnimation();
        }

        return true;
    }

    public void Throw()
    {
        _bombFire.BombFire();
    }
}
