using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_Gold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldUIText;

    private float _duration = 0.5f;
    private int _currentDisplayGold = 0;
    private Tweener _goldTweener;

    private void Start()
    {
        GoldManager.Instance.Bind(UpdateGoldUIText);
    }

    private void OnDestroy()
    {
        if (GoldManager.Instance == null) return;
        GoldManager.Instance.UnBind(UpdateGoldUIText);
    }

    private void UpdateGoldUIText(int targetGold)
    {
        if (_goldTweener != null && _goldTweener.IsActive())
        {
            _goldTweener.Kill();
        }

        _goldTweener = DOTween.To(() => _currentDisplayGold, x =>
        {
            _currentDisplayGold = x;
            _goldUIText.text = _currentDisplayGold.ToString("N0");
        }, targetGold, _duration)
        .SetEase(Ease.OutExpo);
    }
}
