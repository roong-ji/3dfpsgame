using UnityEngine;

public struct Damage
{
    public float Amount;
    public Vector3 HitPoint;
    public Vector3 AttackerPoint;
    public GameObject Attacker;
    public float KnockbackPower;

    public Damage(float amount, Vector3 hitPoint, Vector3 transform, GameObject attacker, float knockbackPower = 0)
    {
        Amount = amount;
        HitPoint = hitPoint;
        AttackerPoint = transform;
        Attacker = attacker;
        KnockbackPower = knockbackPower;
    }
}