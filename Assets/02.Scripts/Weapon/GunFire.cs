using UnityEngine;

public class GunFire : MonoBehaviour
{
    [Header("발사 이펙트 프리팹")]
    [SerializeField] private GameObject[] _effectPrefabs;
    [SerializeField] private Transform _effectTransform;

    private ParticleSystem _hitEffect;
    private float _nextFireTime = 0f;

    private Transform _fireTransform;
    private CameraRecoil _cameraRecoil;

    private Damage _damage;
    private float _fireRate;

    public bool IsReady => Time.time >= _nextFireTime;

    private void Awake()
    {
        _fireTransform = Camera.main.transform;
        _cameraRecoil = Camera.main.GetComponent<CameraRecoil>();
        _hitEffect = EffectManager.Instance.BulletHitEffect;
    }

    public void Initialize(GunStats stats, GameObject owner)
    {
        _damage.Amount = stats.Damage.Value;
        _damage.KnockbackPower = stats.KnockbackPower.Value;
        _damage.Attacker = owner;
        _fireRate = stats.FireRate.Value;
        _cameraRecoil.Initialize(stats.Recoil);
    }

    public void Fire()
    {
        FireEffect();

        // 1. Ray를 생성하고 발사할 위치, 방향, 거리를 설정한다.
        Ray ray = new Ray(origin: _fireTransform.position, direction: _fireTransform.forward);

        // 2. 충돌할 대상의 정보를 저장할 변수를 생성한다.
        RaycastHit hitInfo = new RaycastHit();

        // 3. 발사한다.
        bool isHit = Physics.Raycast(ray, out hitInfo);
        if (isHit)
        {
            // 4. 충돌했다면 피격 이펙트를 표시한다.
            _hitEffect.transform.position = hitInfo.point;
            _hitEffect.transform.forward = hitInfo.normal;

            _hitEffect.Play();

            if (hitInfo.collider.TryGetComponent<IDamagable>(out IDamagable hitObject))
            {
                _damage.HitPoint = hitInfo.point;
                _damage.AttackerPoint = transform.position;
                hitObject.TryTakeDamage(_damage);
            }
        }

        _nextFireTime = Time.time + (1f / _fireRate);
    }

    private void FireEffect()
    {
        _cameraRecoil.CameraRecoilByFire();

        int randomEffect = Random.Range(0, _effectPrefabs.Length);

        GameObject effect = PoolManager.Instance.Spawn(_effectPrefabs[randomEffect], _effectTransform.position);

        effect.transform.rotation = _effectTransform.rotation * Quaternion.Euler(0, 0, 90);
    }
}
