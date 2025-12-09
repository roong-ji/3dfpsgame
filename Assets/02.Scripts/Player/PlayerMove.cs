using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private float _originMoveSpeed = 7f;
    private float _currentMoveSpeed;
    private float _moveSpeedFactor = 2f;
    private float _sprintStaminaCost = 100f;

    private const float Gravity = -9.81f;
    private float _yVelocity = 0;
    private float _jumpPower = 5f;
    private float _duobleJumpStaminaCost = 50f;
    private bool _canDoubleJump = false;

    private CharacterController _controller;
    private PlayerStats _stats;
    private PlayerStamina _stamina;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<PlayerStats>();
        _stamina = GetComponent<PlayerStamina>();
        _currentMoveSpeed = _originMoveSpeed;
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
            _yVelocity = _jumpPower;
            _canDoubleJump = true;
        }
        else if (_canDoubleJump && _stamina.TryUseStamina(_duobleJumpStaminaCost))
        {
            _yVelocity = _jumpPower;
            _canDoubleJump = false;
        }
    }

    private void HandleMoveSpeed()
    {
        bool isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (isShiftDown && _stamina.TryUseStamina(_sprintStaminaCost * Time.deltaTime))
        {
            _currentMoveSpeed = _originMoveSpeed * _moveSpeedFactor;
            return;
        }

        _currentMoveSpeed = _originMoveSpeed;
    }
}
