using UnityEngine;

public interface IPoolable
{
    void Initialize(GameObject prefab);

    void Release();
}
