using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private float _recoilMinX = 2.5f;
    private float _recoilMaxX = 5f;
    private float _recoilY = 0.5f;
    private float _returnSpeed = 5f;

    private Vector3 _recoilOffset;
    public Vector3 RecoilOffset => _recoilOffset;

    private void Update()
    {
        _recoilOffset = Vector3.Lerp(_recoilOffset, Vector3.zero, _returnSpeed * Time.deltaTime);
    }

    public void CameraRecoilByFire()
    {
        float x = Random.Range(-_recoilMinX, -_recoilMaxX);
        float y = Random.Range(-_recoilY, _recoilY);

        _recoilOffset += new Vector3(x, y, 0);
    }
}
