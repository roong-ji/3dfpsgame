using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable
{
    private GameObject _prefab;
    [SerializeField] private GameObject _explosionEffectPrefab;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PoolManager.Instance.Spawn(_explosionEffectPrefab, transform.position);
        Release();
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
