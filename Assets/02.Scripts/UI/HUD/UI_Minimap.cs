using UnityEngine;

public class UI_Minimap : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RangeValue _zoomSize;

    private void Awake()
    {
        _zoomSize.Value = _camera.orthographicSize;
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
