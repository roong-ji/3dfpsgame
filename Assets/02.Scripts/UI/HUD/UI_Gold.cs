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
        if (_goldTweener == null)
        {
            _goldTweener = DOTween.To(() => _currentDisplayGold, SetGoldText, targetGold, _duration)
                .SetEase(Ease.OutExpo)
                .SetAutoKill(false);
        }
        else
        {
            _goldTweener.ChangeValues(_currentDisplayGold, targetGold, _duration).Restart();
        }
    }

    private void SetGoldText(int gold)
    {
        _currentDisplayGold = gold;
        _goldUIText.SetText("{0}", gold);
    }
}
