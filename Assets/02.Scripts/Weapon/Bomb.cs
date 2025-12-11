using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable
{
    private GameObject _prefab;
    private ParticleSystem _explosionEffect;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        _explosionEffect = EffectManager.Instance.BombExplosionEffect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _explosionEffect.transform.position = transform.position;
        _explosionEffect.Play(); 
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
