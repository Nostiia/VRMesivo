using Fusion;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private NetworkRunner _runner;

    [SerializeField] private TMP_InputField _roomNameInput;
    [SerializeField] private TMP_Text _statusText;

    private NetworkSceneManagerDefault _sceneManager;

    private void Start()
    {
        if (_runner == null)
        {
            _runner = gameObject.AddComponent<NetworkRunner>();
            DontDestroyOnLoad(_runner);
        }
    }
    public async void ConnectRoom()
    {
        if (_runner.IsRunning) return;

        _sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        string roomName = _roomNameInput.text;
        _statusText.text = "Connecting to room...";

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = roomName,
            PlayerCount = 2,
            Scene = SceneRef.FromIndex(1), 
            SceneManager = _sceneManager 
        };

        await _runner.StartGame(startGameArgs);
        _statusText.text = "Connected: " + roomName;
    }
}
