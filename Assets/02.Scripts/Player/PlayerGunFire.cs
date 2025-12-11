using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    [Header("현재 장착 중인 총기")]
    [SerializeField] private NorseGun _gun;

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        GunFire();
    }

    private void GunFire()
    {
        _gun.Fire();
    }
}
