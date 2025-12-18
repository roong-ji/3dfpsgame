using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void EndDeathAniamtion()
    {
        GameManager.Instance.GameOver();
    }

    private void ApplyThrow()
    {
        _player.Throw();
    }
}
