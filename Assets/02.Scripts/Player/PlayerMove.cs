using System;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(PlayerStats))]
public class PlayerMove : MonoBehaviour
{
    [Serializable]
    public class moveConfig
    {

    }

    private const float Gravity = -9.81f;

    private float _currentMoveSpeed;
    private float _yVelocity = 0;

    private float _runStamina = 75f;
    private float _jumpStamina = 50f;

    private bool _canDoubleJump = false;

    private CharacterController _controller;
    private PlayerStats _stats;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        HandleJump();
        HandleMoveSpeed();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);

        direction = transform.TransformDirection(direction);
        direction.y = _yVelocity;

        _controller.Move(motion: direction * _currentMoveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        _yVelocity += Gravity * Time.deltaTime;

        // if (_controller.collisionFlags == CollisionFlags.Below)
        if (!Input.GetButtonDown("Jump")) return;

        if (_controller.isGrounded)
        {
            _yVelocity = _stats.JumpPower.Value;
            _canDoubleJump = true;
        }
        else if (_canDoubleJump && _stats.Stamina.TryConsume(_jumpStamina))
        {
            _yVelocity = _stats.JumpPower.Value;
            _canDoubleJump = false;
        }
    }

    private void HandleMoveSpeed()
    {
        bool isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (isShiftDown && _stats.Stamina.TryConsume(_runStamina * Time.deltaTime))
        {
            _currentMoveSpeed = _stats.RunSpeed.Value;
            return;
        }

        _currentMoveSpeed = _stats.MoveSpeed.Value;
    }
}
