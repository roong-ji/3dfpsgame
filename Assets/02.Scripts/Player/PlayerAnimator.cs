using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int s_speed = Animator.StringToHash("Speed");
    private static readonly int s_fire = Animator.StringToHash("Fire");
    private static readonly int s_jump = Animator.StringToHash("Jump");
    private static readonly int s_endJump = Animator.StringToHash("EndJump");
    private static readonly int s_throw = Animator.StringToHash("Throw");
    private static readonly int s_death = Animator.StringToHash("Death");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayMoveAnimation(float speed)
    {
        _animator.SetFloat(s_speed, speed);
    }

    public void PlayFireAnimation()
    {
        _animator.SetTrigger(s_fire);
    }

    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(s_jump);
    }

    public void StopJumpAnimation()
    {
        _animator.SetTrigger(s_endJump);
    }


    public void PlayThrowAnimation()
    {
        _animator.SetTrigger(s_throw);
    }

    public void PlayDeathAnimation()
    {
        for (int i = 1; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0f);
        }
        _animator.applyRootMotion = true;
        _animator.SetTrigger(s_death);
    }

    public void InjureAnimationRate(float rate)
    {
        _animator.SetLayerWeight(2, rate);
    }
}
