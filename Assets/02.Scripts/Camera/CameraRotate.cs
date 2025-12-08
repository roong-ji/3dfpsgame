using UnityEngine;

// 마우스를 조작하면 카메라를 그 방향으로 회전
public class CameraRotate : MonoBehaviour
{
    private const float RotationSpeed = 200f;

    private float _accumulationX = 0;
    private float _accumulationY = 0;

    public float AccumulationX
    {
        get { return _accumulationX; }
    }

    private void Update()
    {
        if (!Input.GetMouseButton(1)) return;

        // 1. 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 2. 마우스 입력을 누적한다.
        _accumulationX += mouseX * RotationSpeed * Time.deltaTime;
        _accumulationY += -mouseY * RotationSpeed * Time.deltaTime;

        // 3. -90 ~ 90도 사이로 시야 각을 제한한다.
        _accumulationY = Mathf.Clamp(_accumulationY, min: -90f, max: 90f);

        // 4. 누적한 회전 방향으로 카메라 회전하기
        transform.eulerAngles = new Vector3(_accumulationY, _accumulationX, 0);


        //Vector3 direction = new Vector3(-mouseY, mouseX, 0);
        //Vector3 eulerAngle = transform.eulerAngles + RotationSpeed * direction * Time.deltaTime;
        //eulerAngle.y = Mathf.Clamp(eulerAngle.y, min: -90f, max: 90f);

        //transform.eulerAngles = eulerAngle;
        // => -1이 359으로 자동 변환된다.
    }
}
