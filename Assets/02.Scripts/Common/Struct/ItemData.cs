using UnityEngine;

public struct ItemData
{
    public GameObject Prefab;
    public GameObject Dropper;
    public int Min;
    public int Max;

    public ItemData(GameObject prefab, GameObject dropper, int min, int max)
    {
        Prefab = prefab;
        Dropper = dropper;
        Min = min;
        Max = max;
    }
}