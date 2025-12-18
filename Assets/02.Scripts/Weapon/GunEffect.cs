using UnityEngine;

public class GunEffect : MonoBehaviour, IPoolable
{
    private GameObject _prefab;

    [Header("이펙트 시간")]
    [SerializeField] private float _duration = 0.8f;

    private bool _shouldRelease = false;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        _shouldRelease = true;
    }

    private void OnEnable()
    {
        if (!_shouldRelease) return;
        Invoke(nameof(Release), _duration);
    }

    public void Release()
    {
        PoolManager.Instance.Release(_prefab, gameObject);
    }
}
