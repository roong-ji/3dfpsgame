using UnityEngine;
using DG.Tweening;

public class CameraMode : Singleton<CameraMode>
{
    // Todo: 카메라 매니저 혹은 카메라 설정 값을 저장하는 클래스 분리
    private Transform _target;
    private ECameraMode _mode = ECameraMode.FPS;
    public ECameraMode Mode => _mode;

    [SerializeField] private Transform _fpsTransform;
    [SerializeField] private Transform _tpsTransform;
    [SerializeField] private Transform _topViewTransform;

    private float _transitionDuration = 1f;
    private Ease _easeType = Ease.OutQuad;
    private bool _isChanging = false;

    private void Start()
    {
        _target = _fpsTransform;
    }

    private void Update()
    {
        InputChangeTarget();
    }

    private void LateUpdate()
    {
        if( _isChanging) return;
        transform.position = _target.position;
    }

    private void InputChangeTarget()
    {
        if (!Input.GetKeyDown(KeyCode.T)) return;
        ChangeMode();
    }

    private void ChangeMode()
    {
        GetNextTarget();
        _isChanging = true;
        
        Vector3 startPosition = transform.position;
        transform.DOKill();

        DOVirtual.Float(0f, 1f, _transitionDuration, (float easedTime) =>
        {
            transform.position = Vector3.Lerp(startPosition, _target.position, easedTime);
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

        _target = _mode switch
        {
            ECameraMode.FPS => _fpsTransform,
            ECameraMode.TPS => _tpsTransform,
            _ => _topViewTransform
        };

        if (_mode == ECameraMode.TopView)
        {
            CursorManager.Instance.UnlockCursor();
        }
        else
        {
            CursorManager.Instance.LockCursor();
        }

        if (_mode == ECameraMode.TPS) return;
        GameManager.Instance.ToggleAutoMode();
    }
}
