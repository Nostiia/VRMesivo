using Fusion;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerAmmo : NetworkBehaviour
{
    [Networked] public int Ammo { get; set; } = 5;
    [SerializeField] private TMP_Text _ammoText;
    private int _ammoMax;


    private void Start()
    {
        _ammoMax = Ammo;
        UpdateAmmoUI();
    }

    public int GetAmmoMax()
    {
        return _ammoMax;
    }

    public void DecrementAmmo()
    {
        DecrementAmmoRpc();
    }

    public void SetDefault()
    {
        SetDefaultRpc();
    }

    public void UpdateAmmoUI()
    {
        if (_ammoText != null)
        {
            _ammoText.text = $"Ammo: {Ammo} / {_ammoMax}";
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DecrementAmmoRpc()
    {
        Ammo--;
        UpdateAmmoUI();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetDefaultRpc()
    {
        Ammo = _ammoMax;
        UpdateAmmoUI();
    }
}
