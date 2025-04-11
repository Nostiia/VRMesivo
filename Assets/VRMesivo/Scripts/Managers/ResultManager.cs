using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _drawResultScreen;

    [SerializeField] private GameObject _gameCanvas;

    private const string _sceneName = "StartGame";

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void ActivateWinScreenRPC() 
    {
        _gameCanvas.SetActive(false);
        _winScreen.SetActive(true);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void ActivateLoseScreenRPC() 
    {
        _gameCanvas.SetActive(false);
        _loseScreen.SetActive(true);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void ActivateDrawResultRPC()
    {
        _gameCanvas.SetActive(false);
        _drawResultScreen.SetActive(true);
    }

    public void BackToMain()
    {
        NetworkRunner runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            runner.Shutdown();
        }
        SceneManager.LoadScene(_sceneName);
    }
}
