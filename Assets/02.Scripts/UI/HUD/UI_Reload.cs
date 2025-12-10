using UnityEngine;
using UnityEngine.UI;

public class UI_Reload : MonoBehaviour
{
    private Slider _reloadUI;

    private void Awake()
    {
        _reloadUI = GetComponent<Slider>();
        PlayerReload.AddListener(UpdateReloadUI);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerReload.RemoveListener(UpdateReloadUI);
    }

    private void UpdateReloadUI(float reloadRate)
    {
        if (reloadRate == 0)
        {
            gameObject.SetActive(true);
        }
        else if (reloadRate == 1)
        {
            gameObject.SetActive(false);
        }
        _reloadUI.value = reloadRate;
    }
}
