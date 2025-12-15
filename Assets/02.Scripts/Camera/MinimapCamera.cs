using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetY = 10f;

    private Camera _camera;
    [SerializeField] private RangeValue _zoomSize;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _zoomSize.Value = _camera.orthographicSize;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position;
        Vector3 finalPosition = _target.position + new Vector3(0, _offsetY, 0);

        transform.position = finalPosition;

        Vector3 targetAngle = _target.eulerAngles;
        targetAngle.x = 90f;

        transform.eulerAngles = targetAngle;
    }

    public void OnClickZoomIn()
    {
        _camera.orthographicSize = --_zoomSize.Value;
    }

    public void OnClickZoomOut()
    {
        _camera.orthographicSize = ++_zoomSize.Value;
    }
}
