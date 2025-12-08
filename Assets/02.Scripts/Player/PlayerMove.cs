using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _moveSpeed = 7f;

    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;   
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction = _cameraTransform.TransformDirection(direction);
        direction.y = 0;

        transform.position += direction * _moveSpeed * Time.deltaTime;
    }
}
