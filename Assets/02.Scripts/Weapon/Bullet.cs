using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이펙트 시간")]
    [SerializeField] private float _duration = 0.05f;

    private LineRenderer _line;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    public void PlayBulletEffect(Vector3 startPosition, Vector3 endPosition)
    {
        _line.SetPosition(0, startPosition);
        _line.SetPosition(1, endPosition);

        Invoke(nameof(DeleteLine), _duration);
    }

    private void DeleteLine()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
    }
}
