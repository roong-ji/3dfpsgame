using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    public class PoolInfo
    {
        public GameObject prefab;
        public int count;
    }

    [SerializeField] private List<PoolInfo> _poolInfos;
    private Dictionary<GameObject, IObjectPool<GameObject>> _pools = new Dictionary<GameObject, IObjectPool<GameObject>>();

    protected override void Init()
    {
        base.Init();
        InitPools();
    }

    private void InitPools()
    {
        foreach (var poolInfo in _poolInfos)
        {
            Transform rootTransform = new GameObject(poolInfo.prefab.name).transform;
            rootTransform.SetParent(transform);

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => 
                {
                    var obj = Instantiate(poolInfo.prefab, rootTransform);
                    if (obj.TryGetComponent(out IPoolable poolable))
                    {
                        poolable.Initialize(poolInfo.prefab);
                    }
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: poolInfo.count
            );

            _pools.Add(poolInfo.prefab, pool);

            List<GameObject> tempObjects = new List<GameObject>();
            for (int i = 0; i < poolInfo.count; i++)
            {
                tempObjects.Add(pool.Get());
            }

            foreach (var obj in tempObjects)
            {
                pool.Release(obj);
            }
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 position)
    {
        GameObject gameObject = _pools[prefab].Get();
        gameObject.transform.position = position;

        return gameObject;
    }

    public void Release(GameObject prefab, GameObject instance)
    {
        if (_pools.ContainsKey(prefab))
        {
            _pools[prefab].Release(instance);
        }
        else
        {
            Destroy(instance);
        }
    }
}
