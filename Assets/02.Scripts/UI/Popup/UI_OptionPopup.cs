using UnityEngine;
using UnityEngine.UI;

public class UI_OptionPopup : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _continueButton.onClick.AddListener(GameContinue);
        _restartButton.onClick.AddListener(GameRestart);
        _exitButton.onClick.AddListener(GameExit);
        Hide();
    }

    private void OnDestroy()
    {
        _continueButton?.onClick.RemoveListener(GameContinue);
        _restartButton?.onClick.RemoveListener(GameRestart);
        _exitButton?.onClick.RemoveListener(GameExit);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void GameContinue()
    {
        GameManager.Instance.Continue();
        Hide();
    }

    private void GameRestart()
    {
        GameManager.Instance.Restart();
    }

    private void GameExit()
    {
        GameManager.Instance.Quit();
    }
}
