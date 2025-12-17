using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    // Todo: FPS TPS 탑뷰 타겟 나누어 명확히 하기
    [SerializeField] private Transform[] _targets;
    private Transform _currentTarget;
    private ECameraMode _mode = ECameraMode.FPS;

    private float _transitionDuration = 1f;
    private Ease _easeType = Ease.OutQuad;
    private bool _isChanging = false;

    private void Start()
    {
        _currentTarget = _targets[(int)_mode];
    }

    private void Update()
    {
        InputChangeTarget();
    }

    private void LateUpdate()
    {
        if( _isChanging) return;
        transform.position = _currentTarget.position;
    }

    private void InputChangeTarget()
    {
        if (!Input.GetKeyDown(KeyCode.T)) return;
        ChangeTarget();
    }

    private void ChangeTarget()
    {
        GetNextTarget();
        _isChanging = true;
        
        Vector3 startPosition = transform.position;
        transform.DOKill();

        DOVirtual.Float(0f, 1f, _transitionDuration, (float easedTime) =>
        {
            transform.position = Vector3.Lerp(startPosition, _currentTarget.position, easedTime);
        })
        .SetEase(_easeType)
        .OnComplete(() => _isChanging = false);
    }

    private void GetNextTarget()
    {
        _mode = _mode switch
        {
            ECameraMode.FPS => ECameraMode.TPS,
            ECameraMode.TPS => ECameraMode.TopView,
            _ => ECameraMode.FPS
        };

        _currentTarget = _targets[(int)_mode];

        if (_mode == ECameraMode.TopView) return;
        GameManager.Instance.ToggleAutoMode();
    }
}
