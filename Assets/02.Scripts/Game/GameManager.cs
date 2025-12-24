using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private EGameState _state = EGameState.Ready;
    private event Action<EGameState> _onGameStateChanged;

    public EGameState State
    {
        get => _state;
        private set
        {
            if (_state == value) return;
            _state = value;
            _onGameStateChanged?.Invoke(_state);
        }
    }

    private bool _autoMode = false;
    public bool AutoMode => _autoMode;

    public event Action<bool> OnAutoModeChanged;

    [Header("시작 대기 시간 설정")]
    [SerializeField] private float _readyTime = 2f;
    [SerializeField] private float _startTime = 0.5f;

    [Header("UI 옵션 팝업")]
    [SerializeField] private UI_OptionPopup _optionUI;

    private void Start()
    {
        StartCoroutine(StartToPlay_Coroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        CursorManager.Instance.UnlockCursor();
        _optionUI.Show();
    }

    public void Continue()
    {
        Time.timeScale = 1;
        CursorManager.Instance.LockCursor();
    }

    public void Restart()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif    
    }

    private IEnumerator StartToPlay_Coroutine()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_readyTime);

        State = EGameState.Start;

        yield return new WaitForSecondsRealtime(_startTime);

        State = EGameState.Playing;
        Time.timeScale = 1;
    }

    public void ToggleAutoMode()
    {
        _autoMode = !_autoMode;
        OnAutoModeChanged?.Invoke(_autoMode);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        State = EGameState.GameOver;
    }

    public void AddListener(Action<EGameState> listener)
    {
        _onGameStateChanged += listener;
    }

    public void RemoveListener(Action<EGameState> listener)
    {
        _onGameStateChanged -= listener;
    }
}
