using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private ParticleSystem _bombExplosionEffect;
    [SerializeField] private ParticleSystem _bulletHitEffect;

    public ParticleSystem BombExplosionEffect => _bombExplosionEffect;

    public ParticleSystem BulletHitEffect => _bulletHitEffect;
}
