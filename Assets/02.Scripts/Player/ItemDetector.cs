using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            coin.Magnet(transform);
        }
    }
}
