using UnityEngine;

public class UI_Crosshair : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnAutoModeChanged += OnOffCrosshairUI;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnAutoModeChanged -= OnOffCrosshairUI;
    }

    private void OnOffCrosshairUI(bool autoMode)
    {
        gameObject.SetActive(!autoMode);
    }
}
