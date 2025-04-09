using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _drawResultScreen;

    private const string _sceneName = "StartGame";

    public void ActivateWinScreen() 
    {
            _winScreen.SetActive(true);
    }

    public void ActivateLoseScreen() 
    {
            _loseScreen.SetActive(true);
    }

    public void ActivateDrawResult()
    {
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
