using UnityEngine;

public class BombEffect : MonoBehaviour, IPoolable
{
    private GameObject _prefab;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
    }

    private void OnParticleSystemStopped()
    {
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
