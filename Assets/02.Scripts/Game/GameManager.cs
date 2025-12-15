using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private EGameState _state = EGameState.Ready;
    private event Action<EGameState> _onGameStateChanged;

    [Header("시작 대기 시간 설정")]
    [SerializeField] private float _readyTime = 2f;
    [SerializeField] private float _startTime = 0.5f;

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

    public void AddListener(Action<EGameState> listener)
    {
        _onGameStateChanged += listener;
    }

    public void RemoveListener(Action<EGameState> listener)
    {
        _onGameStateChanged -= listener;
    }
}
