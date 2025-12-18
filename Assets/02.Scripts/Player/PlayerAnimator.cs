using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int s_speed = Animator.StringToHash("Speed");
    private static readonly int s_shoot = Animator.StringToHash("Shoot");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayMoveAnimation(float speed)
    {
        _animator.SetFloat(s_speed, speed);
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger(s_shoot);
    }
}
