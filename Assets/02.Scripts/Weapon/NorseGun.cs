using UnityEngine;

public class NorseGun : MonoBehaviour
{
    [SerializeField] private Transform _fireTransform;
    private ParticleSystem _hitEffect;
    private float _nextFireTime = 0f;

    private Transform _mainCameraTransform;
    private CameraRecoil _cameraRecoil;

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
        _cameraRecoil = Camera.main.GetComponent<CameraRecoil>();
        _hitEffect = EffectManager.Instance.BulletHitEffect;
    }

    public void Fire()
    {
        if (Time.time < _nextFireTime) return;

        if (!PlayerStats.Instance.BulletCount.TryConsume(1)) return;

        _cameraRecoil.CameraRecoilByFire();

        // 1. Ray를 생성하고 발사할 위치, 방향, 거리를 설정한다.
        Ray ray = new Ray(origin: _fireTransform.position, direction: _mainCameraTransform.forward);

        // 2. 충돌할 대상의 정보를 저장할 변수를 생성한다.
        RaycastHit hitInfo = new RaycastHit();

        // 3. 발사한다.
        bool isHit = Physics.Raycast(ray, out hitInfo);
        if (isHit)
        {
            // 4. 충돌했다면 피격 이펙트를 표시한다.
            Debug.Log(hitInfo.transform.name);

            _hitEffect.transform.position = hitInfo.point;
            _hitEffect.transform.forward = hitInfo.normal;

            _hitEffect.Play();
        }

        _nextFireTime = Time.time + (1f / PlayerStats.Instance.FireRate.Value);
    }
}
