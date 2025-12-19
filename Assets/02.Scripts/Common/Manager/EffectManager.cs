using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private ParticleSystem _bombExplosionEffect;
    [SerializeField] private ParticleSystem _bulletHitEffect;
    [SerializeField] private ParticleSystem _drumExplosionEffect;

    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private ParticleSystem[] _bloodEffects;
    [SerializeField] private Bullet _bulletEffect;

    public ParticleSystem BombExplosionEffect => _bombExplosionEffect;

    public ParticleSystem BulletHitEffect => _bulletHitEffect;

    public ParticleSystem DrumExplosionEffect => _drumExplosionEffect;

    public Bullet BulletEffect => _bulletEffect;

    public void PlayBloodEffect(Vector3 position, Vector3 normal)
    {
        _bloodEffect.transform.position = position;
        _bloodEffect.transform.forward = normal;

        foreach (var effect in _bloodEffects)
        {
            effect.Play();
        }
    }
}
