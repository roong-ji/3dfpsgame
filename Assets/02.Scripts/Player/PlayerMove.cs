using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _moveSpeed = 7f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction = transform.TransformDirection(direction);

        transform.position += direction * _moveSpeed * Time.deltaTime;
    }
}
