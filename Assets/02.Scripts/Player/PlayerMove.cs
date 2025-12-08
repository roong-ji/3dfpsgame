using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private float _moveSpeed = 7f;

    private const float Gravity = -9.81f;
    private float _yVelocity = 0;
    private float _jumpPower = 5f;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
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

        _controller.Move(motion: direction * _moveSpeed * Time.deltaTime);
    }
}
