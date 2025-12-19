using UnityEngine;

public class UI_Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject _normalCrosshair;
    [SerializeField] private GameObject _zoomInCrosshair;
    private Camera _mainCamera;
    private float _normalFOV;
    private float _zoomFOV = 10f;

    private void Start()
    {
        _mainCamera = Camera.main;
        _normalFOV = _mainCamera.fieldOfView;
        GameManager.Instance.OnAutoModeChanged += OnOffCrosshairUI;
        PlayerGunController.OnZoomModeChanged += ChangeCrosshairUI;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnAutoModeChanged -= OnOffCrosshairUI;
        PlayerGunController.OnZoomModeChanged -= ChangeCrosshairUI;
    }

    private void OnOffCrosshairUI(bool autoMode)
    {
        gameObject.SetActive(!autoMode);
    }

    private void ChangeCrosshairUI(EZoomMode zoomMode)
    {
        _normalCrosshair.SetActive(!_normalCrosshair.activeSelf);
        _zoomInCrosshair.SetActive(!_zoomInCrosshair.activeSelf);

        _mainCamera.fieldOfView = zoomMode switch 
        {
            EZoomMode.Normal => _normalFOV,
            _ => _zoomFOV
        };
    }
}
