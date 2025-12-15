using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_BloodScreen : MonoBehaviour
{
    [SerializeField] Player _player;

    private Image _bloodScreenImage;
    [SerializeField] private float _effectDuration = 0.5f;

    private Tween _fadeTween;

    private void Awake()
    {
        _bloodScreenImage = GetComponent<Image>();
        _player.OnTakeDamaged += PlayBloodScreenEffect;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _player.OnTakeDamaged -= PlayBloodScreenEffect;
        _fadeTween?.Kill();
    }

    private void PlayBloodScreenEffect()
    {
        gameObject.SetActive(true);

        _fadeTween?.Kill();

        Color color = _bloodScreenImage.color;
        color.a = 1f;
        _bloodScreenImage.color = color;

        _fadeTween = _bloodScreenImage.DOFade(0f, _effectDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => gameObject.SetActive(false)
            );
    }
}
