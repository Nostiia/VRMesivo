using Fusion;
using TMPro;
using UnityEngine;

public class TimerManager : NetworkBehaviour
{
    [Networked] private TickTimer TrainingTimer { get; set; }
    [Networked] public int ReadyPlayers { get; private set; } = 0;
    [Networked] private TickTimer GameTimer { get; set; }

    private const string _timer = "TimerText";
    private TMP_Text _timerText;

    private readonly int _trainingDurations = 30;
    private readonly int _gameDurations = 300;

    public bool IsGameTime { get; private set; } = false;
    private bool _isStarted = false;

    [SerializeField] private GameController _gameController;
    private ResultManager _resultManager;

    public override void Spawned()
    {
        _timerText = GameObject.Find(_timer).GetComponent<TMP_Text>();
        _resultManager = FindObjectOfType<ResultManager>();
    }
    public void StartGame()
    {
        _isStarted = true;
        StartTraining();
    }

    public void IncrementPlayers()
    {
        RPC_IncrementPlayers();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_IncrementPlayers()
    {
        ReadyPlayers++;
        Debug.Log(ReadyPlayers);
    }

    public void StartTraining()
    {
        TrainingTimer = TickTimer.CreateFromSeconds(Runner, _trainingDurations);
    }

    private void StartGameTimer()
    {
        PlayerDefault[] players = FindObjectsOfType<PlayerDefault>();
        foreach (PlayerDefault player in players)
        {
            if (player != null)
            {
                player.SetDefault();
            }
        }
        _gameController.StartGameTime();
        IsGameTime = true;
        GameTimer = TickTimer.CreateFromSeconds(Runner, _gameDurations);
    }

    public override void FixedUpdateNetwork()
    {
        Debug.Log(ReadyPlayers);
        if (!_isStarted && ReadyPlayers == 2)
        {
            _gameController.StartTraining();
            StartGame();
        }
        if (!IsGameTime)
        {
            HandleTrainingTimer();
        }
        else
        {
            HandleGameTimer();
        }
    }

    private void HandleTrainingTimer()
    {
        if (TrainingTimer.IsRunning)
        {
            if (TrainingTimer.Expired(Runner))
            {
                StartGameTimer();
            }
            else
            {
                int remainingTime = Mathf.CeilToInt(TrainingTimer.RemainingTime(Runner) ?? 0);
                RPC_UpdateTimer(remainingTime);
            }
        }
        else
        {
            Debug.LogWarning("Wave timer not running properly.");
        }
    }

    private void HandleGameTimer()
    {
        if (GameTimer.IsRunning)
        {
            if (GameTimer.Expired(Runner))
            {
                Debug.Log("Draw result");
                _resultManager.ActivateDrawResult();
            }
            else
            {
                int remainingTime = Mathf.CeilToInt(GameTimer.RemainingTime(Runner) ?? 0);
                RPC_UpdateTimer(remainingTime);
            }
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_UpdateTimer(int time)
    {
        if (_timerText)
        {
            int minutes = time / 60;
            int seconds = time % 60;
            _timerText.text = $"{minutes:0}:{seconds:00}";
        }
    }
}
