using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterHealthBar : MonoBehaviour
{
    private Monster _monster;
    private Transform _mainCameraTransform;

    [Header("체력 UI")]
    [SerializeField] private Image _gaugeImage;

    [Header("연출 UI")]
    [SerializeField] private Image _effectImage;
    [SerializeField] private Image _splashEffectImage;

    [Header("연출 설정")]
    [SerializeField] private float _hitDelay = 0.5f;
    [SerializeField] private float _hitDuration = 0.5f;
    [SerializeField] private float _fadeDuration = 0.2f;

    private Sequence _hitSequence;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
        _mainCameraTransform = Camera.main.transform;

        _monster.Health.AddListener(UpdateHealthUI);
        _monster.OnTakeDamaged += PlayHealthUIEffect;
    }

    private void OnDestroy()
    {
        _monster.Health.RemoveListener(UpdateHealthUI);
        _monster.OnTakeDamaged -= PlayHealthUIEffect;

        _hitSequence?.Kill();
    }

    private void LateUpdate()
    {
        if (_monster.IsDead && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.forward = _mainCameraTransform.forward;
    }

    private void UpdateHealthUI(float health, float maxHealth)
    {
        _gaugeImage.fillAmount = health / maxHealth;
    }

    private void PlayHealthUIEffect()
    {
        if (_hitSequence != null && _hitSequence.IsActive())
        {
            _hitSequence.Kill();
        }
        else
        {
            _effectImage.gameObject.SetActive(true);
            _splashEffectImage.gameObject.SetActive(true);
        }

        _hitSequence = DOTween.Sequence();

        _hitSequence.Insert(0, _splashEffectImage.DOFade(0f, _fadeDuration).From(1f).SetEase(Ease.Linear));

        _hitSequence.Insert(_hitDelay, _effectImage.DOFillAmount(_gaugeImage.fillAmount, _hitDuration).SetEase(Ease.OutQuad));

        _hitSequence.OnComplete(() => 
        {
            _effectImage.gameObject.SetActive(false);
            _splashEffectImage.gameObject.SetActive(false);
        });
    }
}
