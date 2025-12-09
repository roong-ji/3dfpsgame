using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private float _originMoveSpeed = 7f;
    private float _currentMoveSpeed;
    private float _moveSpeedFactor = 2f;
    private float _sprintStaminaCost = 10;

    private const float Gravity = -9.81f;
    private float _yVelocity = 0;
    private float _jumpPower = 5f;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _currentMoveSpeed = _originMoveSpeed;
    }

    private void Update()
    {
        HandleMoveSpeed();
        HandleMovement();
    }

    private void HandleMovement()
    {
        _yVelocity += Gravity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);

        // if (_controller.collisionFlags == CollisionFlags.Below)
        if (Input.GetButtonDown("Jump") && _controller.isGrounded)
        {
            _yVelocity = _jumpPower;
        }

        direction = transform.TransformDirection(direction);
        direction.y = _yVelocity;

        _controller.Move(motion: direction * _currentMoveSpeed * Time.deltaTime);
    }

    private void HandleMoveSpeed()
    {
        bool isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (isShiftDown && PlayerStamina.Instance.TryUseStamina(_sprintStaminaCost * Time.deltaTime))
        {
            _currentMoveSpeed = _originMoveSpeed * _moveSpeedFactor;
            return;
        }

        _currentMoveSpeed = _originMoveSpeed;
    }
}
