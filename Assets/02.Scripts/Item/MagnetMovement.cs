using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagnetMovement : MonoBehaviour
{
    [Header("연출 시간")]
    [SerializeField] private float _duration = 0.5f;

    [Tooltip("곡선 높이")]
    [SerializeField] private float _arcHeight = 2.0f;

    [Tooltip("제어점 랜덤 범위")]
    [SerializeField] private float _scatterRange = 1.0f;

    private bool _isMoving = false;
    private float _timer;

    private Vector3 _startPosition;
    private Vector3 _controlPosition;
    private Transform _target;
    private Action _onCompleteCallback;

    public void MoveTo(Transform target, Action onComplete)
    {
        _target = target;
        _onCompleteCallback = onComplete;
        _startPosition = transform.position;

        Vector3 midPoint = (_startPosition + _target.position) / 2;
        _controlPosition = midPoint + (Vector3.up * _arcHeight) + (Random.insideUnitSphere * _scatterRange);

        _timer = 0f;
        _isMoving = true;
    }

    private void Update()
    {
        if (!_isMoving) return;

        _timer += Time.deltaTime;
        float timeRate = _timer / _duration;

        if (timeRate >= 1f)
        {
            _isMoving = false;
            _onCompleteCallback?.Invoke();
            return;
        }

        transform.position = CalculateBezierPoint(timeRate, _startPosition, _controlPosition, _target.position);
    }

    private Vector3 CalculateBezierPoint(float timeRate, Vector3 startPosition, Vector3 controlPosition, Vector3 endPosition)
    {
        float oneMinusT = 1f - timeRate;

        float timeSquared = timeRate * timeRate;        // t^2
        float oneMinusTSquared = oneMinusT * oneMinusT; // (1-t)^2

        //          (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
        Vector3 p = (oneMinusTSquared * startPosition)
                  + (2f * oneMinusT * timeRate * controlPosition)
                  + (timeSquared * endPosition);

        return p;
    }

    public void Stop()
    {
        _isMoving = false;
    }
}
