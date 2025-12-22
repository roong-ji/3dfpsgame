using UnityEngine;

public class ItemDropper
{
    public static void DropRandomGolds(ItemData itemData)
    {
        int count = Random.Range(itemData.Min, itemData.Max);

        for (int i = 0; i < count; ++i)
        {
            PoolManager.Instance.Spawn(itemData.Prefab, itemData.Dropper.transform.position);
        }
    }
}
