using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_BloodScreen : MonoBehaviour
{
    [SerializeField] Player _player;

    private Image _bloodScreenImage;
    [SerializeField] private float _effectDuration = 0.5f;

    private Coroutine _bloodScreenCoroutine;

    private void Awake()
    {
        _bloodScreenImage = GetComponent<Image>();
        _player.OnTakeDamaged += PlayBloodScreenEffect;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _player.OnTakeDamaged -= PlayBloodScreenEffect;
    }

    private void PlayBloodScreenEffect()
    {
        gameObject.SetActive(true);

        if (_bloodScreenCoroutine != null)
        {
            StopCoroutine(_bloodScreenCoroutine);
        }

        _bloodScreenCoroutine = StartCoroutine(BloodScreen_Coroutine());
    }

    private IEnumerator BloodScreen_Coroutine()
    {
        Color color = _bloodScreenImage.color;
        color.a = 1f;
        _bloodScreenImage.color = color;

        float timer = 0;
        while (timer < _effectDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / _effectDuration);
            _bloodScreenImage.color = color;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
