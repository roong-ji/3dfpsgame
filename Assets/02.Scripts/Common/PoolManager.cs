using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    public class PoolInfo
    {
        public GameObject Prefab;
        public int Count;
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
            Transform rootTransform = new GameObject(poolInfo.Prefab.name).transform;
            rootTransform.SetParent(transform);

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => 
                {
                    var obj = Instantiate(poolInfo.Prefab, rootTransform);
                    if (obj.TryGetComponent(out IPoolable poolable))
                    {
                        poolable.Initialize(poolInfo.Prefab);
                    }
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: poolInfo.Count
            );

            _pools.Add(poolInfo.Prefab, pool);

            List<GameObject> tempObjects = new List<GameObject>();
            for (int i = 0; i < poolInfo.Count; i++)
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
        GameObject obj = _pools[prefab].Get();
        obj.transform.position = position;

        return obj;
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
