using System;
using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        StartCoroutine(StartToPlay_Coroutine());
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
