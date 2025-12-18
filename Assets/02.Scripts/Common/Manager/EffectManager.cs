using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private ParticleSystem _bombExplosionEffect;
    [SerializeField] private ParticleSystem _bulletHitEffect;
    [SerializeField] private ParticleSystem _drumExplosionEffect;
    [SerializeField] private Bullet _bulletEffect;

    public ParticleSystem BombExplosionEffect => _bombExplosionEffect;

    public ParticleSystem BulletHitEffect => _bulletHitEffect;

    public ParticleSystem DrumExplosionEffect => _drumExplosionEffect;

    public Bullet BulletEffect => _bulletEffect;
}
