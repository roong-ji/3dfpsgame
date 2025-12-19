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

    public void PlayBulletEffect(Line line)
    {
        // Todo: 경로 상의 랜덤한 라인을 그리도록 변경
        _line.SetPosition(0, line.Start);
        _line.SetPosition(1, line.End);

        // Todo: 리플렉션 제거
        Invoke(nameof(DeleteLine), _duration);
    }

    private void DeleteLine()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
    }
}
