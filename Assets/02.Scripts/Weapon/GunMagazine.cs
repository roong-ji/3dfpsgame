[System.Serializable]
public class GunMagazine
{
    public const int Capacity = 30;
    public Resource Bullet;

    public bool IsFull => Bullet.Count == Capacity;

    public int NeedToFill => Capacity - Bullet.Count;
}
