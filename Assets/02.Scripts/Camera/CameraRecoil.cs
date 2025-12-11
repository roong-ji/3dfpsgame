using DG.Tweening;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private RecoilData _recoil;

    private Vector3 _recoilOffset;

    public Vector3 RecoilOffset => _recoilOffset;

    private Tweener _recoilTweener;
    private Ease _returnEase = Ease.OutQuad;

    public void Initialize(RecoilData recoil)
    {
        _recoil = recoil;
    }

    public void CameraRecoilByFire()
    {
        float x = Random.Range(-_recoil.MaxX, -_recoil.MinX);
        float y = Random.Range(-_recoil.Y, _recoil.Y);

        _recoilOffset += new Vector3(x, y, 0);

        _recoil.Duration += _recoil.DurationIncrement;
         _recoil.Duration = Mathf.Min(_recoil.Duration, _recoil.MaxDuration);

        _recoilTweener.Kill();
        _recoilTweener = DOTween.To(
            () => _recoilOffset,
            val => _recoilOffset = val,
            Vector3.zero,
            _recoil.Duration
        )
        .SetEase(_returnEase)
        .OnComplete(() => _recoil.Duration = _recoil.StartDuration);
    }
}
