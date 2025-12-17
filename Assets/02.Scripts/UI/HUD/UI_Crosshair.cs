using UnityEngine;

public class UI_Crosshair : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnAutoModeChanged += OnOffCrosshairUI;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnAutoModeChanged -= OnOffCrosshairUI;
    }

    private void OnOffCrosshairUI(bool autoMode)
    {
        Debug.Log(autoMode);
        gameObject.SetActive(!autoMode);
    }
}
