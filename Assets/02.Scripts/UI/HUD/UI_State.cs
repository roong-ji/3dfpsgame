using UnityEngine;
using TMPro;

public class UI_State : MonoBehaviour
{
    private readonly string _readyTextContent = "준비 중...";
    private readonly string _startTextContent = "시작!";
    private readonly string _gameOverTextContent = "게임 오버";

    private TextMeshProUGUI _stateUIText;

    private void Start()
    {
        _stateUIText = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.AddListener(UpdateStateUIText);
        UpdateStateUIText(EGameState.Ready);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.RemoveListener(UpdateStateUIText);
    }

    private void UpdateStateUIText(EGameState state)
    {
        switch (state)
        {
            case EGameState.Ready:
                _stateUIText.text = _readyTextContent;
                break;

            case EGameState.Start:
                _stateUIText.text = _startTextContent;
                break;
            case EGameState.Playing:
                gameObject.SetActive(false);
                break;
            case EGameState.GameOver:
                gameObject.SetActive(true);
                _stateUIText.text = _gameOverTextContent;
                break;
        }
    }
}
