using Fusion;
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerHP : NetworkBehaviour
{
    [Networked] public int HP { get; set; } = 20;

    [SerializeField] private TMP_Text _hpText;
    private int _hpMax;

    [SerializeField] private ResultManager _resultManager;
    [SerializeField] private PlayerHP _enemy;

    [Networked] public bool _enemyDied { get; set; } = false;

    private GameController _gameController;

    private void Start()
    {
        _resultManager = FindObjectOfType<ResultManager>();
        _enemy = FindObjectOfType<PlayerHP>();
        _gameController = FindAnyObjectByType<GameController>();
        _hpMax = HP;

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
            _resultManager.ActivateLoseScreen();
        }
      
    }
}
