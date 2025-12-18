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

    private float _currentMoveSpeed;
    private float _yVelocity = 0;

    private bool _canDoubleJump = false;
    private bool _isJumping = false;

    private CharacterController _controller;
    private PlayerStats _stats;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (_stats.IsDead || GameManager.Instance.AutoMode) return;

        HandleJump();
        HandleMoveSpeed();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        _animator.PlayMoveAnimation(direction.magnitude);

        direction = transform.TransformDirection(direction);
        direction.y = _yVelocity;

        _controller.Move(motion: direction * _currentMoveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_isJumping && _controller.isGrounded)
        {
            _animator.StopJumpAnimation();
            _isJumping = false;
        }

        _yVelocity += _configs.Gravity * Time.deltaTime;

        if (!Input.GetButtonDown("Jump")) return;

        _animator.PlayJumpAnimation();

        // if (_controller.collisionFlags == CollisionFlags.Below)
        if (_controller.isGrounded)
        {
            _yVelocity = _stats.JumpPower.Value;
            _canDoubleJump = true;
            _isJumping = true;
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
