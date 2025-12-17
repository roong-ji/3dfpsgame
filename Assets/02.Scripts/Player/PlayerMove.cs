using System;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(PlayerStats))]
public class PlayerMove : MonoBehaviour
{
    [Serializable]
    public class moveConfig
    {
        public float Gravity = -9.81f;
        public float RunStamina = 75f;
        public float JumpStamina = 50f;
    }

    private moveConfig _configs = new moveConfig();

    private static readonly int s_speed = Animator.StringToHash("Speed");

    private float _currentMoveSpeed;
    private float _yVelocity = 0;

    private bool _canDoubleJump = false;

    private CharacterController _controller;
    private PlayerStats _stats;
    private Animator _animator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.AutoMode) return;

        HandleJump();
        HandleMoveSpeed();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        _animator.SetFloat(s_speed, direction.magnitude);

        direction = transform.TransformDirection(direction);
        direction.y = _yVelocity;

        _controller.Move(motion: direction * _currentMoveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        _yVelocity += _configs.Gravity * Time.deltaTime;

        // if (_controller.collisionFlags == CollisionFlags.Below)
        if (!Input.GetButtonDown("Jump")) return;

        if (_controller.isGrounded)
        {
            _yVelocity = _stats.JumpPower.Value;
            _canDoubleJump = true;
        }
        else if (_canDoubleJump && _stats.Stamina.TryConsume(_configs.JumpStamina))
        {
            _yVelocity = _stats.JumpPower.Value;
            _canDoubleJump = false;
        }
    }

    private void HandleMoveSpeed()
    {
        bool isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (isShiftDown && _stats.Stamina.TryConsume(_configs.RunStamina * Time.deltaTime))
        {
            _currentMoveSpeed = _stats.RunSpeed.Value;
            return;
        }

        _currentMoveSpeed = _stats.MoveSpeed.Value;
    }
}
