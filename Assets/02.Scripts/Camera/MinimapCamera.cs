using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetY = 10f;
    private const float RotationX = 90f;

    private void LateUpdate()
    {
        transform.position = _target.position + new Vector3(0, _offsetY);
        transform.eulerAngles = new Vector3(RotationX, _target.eulerAngles.y);
    }
}
