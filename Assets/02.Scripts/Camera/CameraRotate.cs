using UnityEngine;

// 마우스를 조작하면 카메라를 그 방향으로 회전
public class CameraRotate : MonoBehaviour
{
    private const float RotationSpeed = 200f;
    private const float LimitRotationY = 90f;

    private float _accumulationX = 0;
    private float _accumulationY = 0;

    private Vector3 _cameraRotation = Vector3.zero;

    public float AccumulationX => _accumulationX;

    private CameraRecoil _recoil;

    private void Awake()
    {
        _recoil = GetComponent<CameraRecoil>();
    }

    private void Update()
    {
        if(!CursorManager.Instance.IsLockCursor) return;

        CameraRotateByMouseInput();
        transform.eulerAngles = _cameraRotation + _recoil.RecoilOffset;
    }

    private void CameraRotateByMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _accumulationX += mouseX * RotationSpeed * Time.deltaTime;
        _accumulationY += -mouseY * RotationSpeed * Time.deltaTime;

        _accumulationY = Mathf.Clamp(_accumulationY, min: -LimitRotationY, max: LimitRotationY);

        _cameraRotation = new Vector3(_accumulationY, _accumulationX, 0);
    }
}
