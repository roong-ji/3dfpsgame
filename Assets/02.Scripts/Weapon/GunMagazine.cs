[System.Serializable]
public class GunMagazine
{
    public const int Capacity = 30;
    public CountStat BulletCount;

    public bool IsFull => BulletCount.Count == Capacity;

    public int NeedToFill => Capacity - BulletCount.Count;
}
