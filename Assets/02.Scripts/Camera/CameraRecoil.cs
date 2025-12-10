using DG.Tweening;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private float _recoilMinX = 2.5f;
    private float _recoilMaxX = 5f;
    private float _recoilY = 0.5f;

    private float _startReturnDuration = 0.2f;
    private float _currentReturnDuration = 0.2f;
    private float _durationIncrement = 0.1f;
    private float _maxReturnDuration = 1f;

    private Vector3 _recoilOffset;
    public Vector3 RecoilOffset => _recoilOffset;

    private Tweener _recoilTweener;
    private Ease _returnEase = Ease.OutQuad;

    public void CameraRecoilByFire()
    {
        float x = Random.Range(-_recoilMinX, -_recoilMaxX);
        float y = Random.Range(-_recoilY, _recoilY);

        _recoilOffset += new Vector3(x, y, 0);

        _currentReturnDuration += _durationIncrement;
        _currentReturnDuration = Mathf.Min(_currentReturnDuration, _maxReturnDuration);

        _recoilTweener.Kill();
        _recoilTweener = DOTween.To(
            () => _recoilOffset,
            val => _recoilOffset = val,
            Vector3.zero,
            _currentReturnDuration
        )
        .SetEase(_returnEase)
        .OnComplete(() => _currentReturnDuration = _startReturnDuration);
    }
}
