using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _throwPower = 15f;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        if (PlayerStats.Instance.BombCount.TryConsume(1))
        {
            GameObject bomb = PoolManager.Instance.Spawn(_bombPrefab, _fireTransform.position);
            Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * _throwPower, ForceMode.Impulse);
        }
    }
}
