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

        _targetIndex = (_targetIndex + 1) % _targets.Length;
        _currentTarget = _targets[_targetIndex];
        _isChanging = true;

        transform.DOMove(_currentTarget.position, _transitionDuration)
            .SetEase(_easeType)
            .OnComplete(() =>
            {
                _isChanging = false;
            });
    }
}
