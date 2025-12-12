using UnityEngine;
using UnityEngine.UI;

public class UI_Reload : MonoBehaviour
{
    private Slider _reloadUI;

    private NorseGun _currentGun;

    private void Awake()
    {
        _reloadUI = GetComponent<Slider>();
        PlayerGunController.AddListener(Initialize);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerGunController.RemoveListener(Initialize);
    }
    public void Initialize(NorseGun gun)
    {
        if (_currentGun != null)
        {
            _currentGun.OnReloadProgress -= UpdateReloadUI;
        }
        _currentGun = gun;
        _currentGun.OnReloadProgress += UpdateReloadUI;
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
