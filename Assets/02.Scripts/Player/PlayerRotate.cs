using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private const float RotationSpeed = 200f;

    [SerializeField] private CameraRotate _cameraRotate;
    private float _accumulationX = 0;

    private void Update()
    {
        if (!Input.GetMouseButton(1)) return;

        _accumulationX = _cameraRotate.AccumulationX;

        transform.eulerAngles = new Vector3(0, _accumulationX);
    }
}
