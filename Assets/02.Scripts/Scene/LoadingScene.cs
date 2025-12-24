using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _progressTextUI;

    private void Start()
    {
        StartCoroutine(LoadScene_Coroutine());
    }

    private IEnumerator LoadScene_Coroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("InGameScene");

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            _progressSlider.value = progress;
            _progressTextUI.SetText("{0:0}%", progress * 100);

            if (progress >= 1)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
