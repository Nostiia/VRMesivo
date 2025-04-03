using Fusion;
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerHP : NetworkBehaviour
{
    [Networked] public int HP { get; set; } = 20;

    private TMP_Text _hpText;
    private int _hpMax;
    private const string HPText = "HPText";

    [SerializeField] private ResultManager _resultManager;
    [SerializeField] private PlayerHP _enemy;

    private GameObject _gameCanvas;

    [Networked] public bool _enemyDied { get; set; } = false;

    private const string _hittedState = "IsHitted";
    [Networked] private bool IsHitted { get; set; }
    private GameController _gameController;

    private void Start()
    {
        _resultManager = FindObjectOfType<ResultManager>();
        _enemy = FindObjectOfType<PlayerHP>();
        _hpText = GameObject.Find(HPText).GetComponent<TMP_Text>();
        _gameCanvas = GameObject.Find("GameCanvas");
        _gameController = FindAnyObjectByType<GameController>();
        _hpMax = HP;
        _gameCanvas.SetActive(true);

        UpdateHPUI();
    }

    public void TakeDamage(int Damage)
    {
        DealDamageRpc(Damage);
        CheckVictory();
    }

    private void CheckVictory()
    {
        if (_enemyDied && _gameController.GameTimeStarts)
        {
            _gameController.EndGame();
            _gameCanvas.SetActive(false);
            _resultManager.ActivateWinScreen();                              
        }
    }

    private void UpdateHPUI()
    {
        if (_hpText != null)
        {
            _hpText.text = $"HP: {HP} / {_hpMax}";
        }
    }

    private IEnumerator HitAnimation()
    {
        IsHitted = true;
        yield return new WaitForSeconds(0.5f);
        IsHitted = false;
    }

    public void SetDefault()
    {
        SetDefaultRpc();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetDefaultRpc()
    {
        HP = _hpMax;
        UpdateHPUI();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        if (HP >= 0)
        {
            HP -= damage;
            StartCoroutine(HitAnimation());
            if (HP <= 0)
            {              
                DieRpc();
            }
            if (HP == 10)
            {
                _enemyDied = true;
            }
            UpdateHPUI();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DieRpc()
    {
        if (_gameController.GameTimeStarts)
        {
            _gameCanvas.SetActive(false);
            _resultManager.ActivateLoseScreen();
        }
      
    }
}
