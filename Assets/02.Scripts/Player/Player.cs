using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private PlayerStats _stats;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
    }

    public bool TryTakeDamage(Damage damage)
    {
        if (_stats.IsDead) return false;

        _stats.ApplyDamage(damage.Amount);
        
        if (_stats.IsDead)
        {
            GameManager.Instance.GameOver();
        }

        return true;
    }

}
