using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private float _moveSpeed = 7f;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction = transform.TransformDirection(direction);

        _controller.Move(motion: direction * _moveSpeed * Time.deltaTime);
    }
}
