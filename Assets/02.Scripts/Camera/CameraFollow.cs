using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform[] _targets;
    private Transform _currentTarget;
    private int _targetIndex = 0;
    private float _transitionDuration = 1f;
    private Ease _easeType = Ease.OutQuad;
    private bool _isChanging = false;

    private void Start()
    {
        _currentTarget = _targets[_targetIndex];
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
        _targetIndex = (_targetIndex + 1) % _targets.Length;
        _currentTarget = _targets[_targetIndex];
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
}
