using UnityEngine;

public class GunFire : MonoBehaviour
{
    private ParticleSystem _hitEffect;
    private float _nextFireTime = 0f;

    private Transform _fireTransform;
    private CameraRecoil _cameraRecoil;

    public bool IsReady => Time.time >= _nextFireTime;

    private void Awake()
    {
        _fireTransform = Camera.main.transform;
        _cameraRecoil = Camera.main.GetComponent<CameraRecoil>();
        _hitEffect = EffectManager.Instance.BulletHitEffect;
    }

    public void Fire()
    {
        _cameraRecoil.CameraRecoilByFire();

        // 1. Ray를 생성하고 발사할 위치, 방향, 거리를 설정한다.
        Ray ray = new Ray(origin: _fireTransform.position, direction: _fireTransform.forward);

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
