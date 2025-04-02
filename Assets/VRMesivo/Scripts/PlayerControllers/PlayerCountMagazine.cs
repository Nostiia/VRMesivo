using Fusion;
using TMPro;
using UnityEngine;

public class PlayerCountMagazine : NetworkBehaviour
{
    [Networked] public int MagazineCount { get; set; } = 0;
    private TMP_Text _magazineText;
    private const string MagazineText = "Magazine";

    [SerializeField] private PlayerAmmo _playerAmmo;

    private void Start()
    {
        _magazineText = GameObject.Find(MagazineText)?.GetComponent<TMP_Text>();
        if (_magazineText == null)
        {
            Debug.LogError("MagazineText UI not found in the scene!");
        }
        UpdateMagazineText();
    }

    private void Update()
    {
        if (!Object.HasStateAuthority) return;
        if (Input.GetKeyDown(KeyCode.X) && MagazineCount > 0)
        {
            _playerAmmo.Ammo = _playerAmmo.GetAmmoMax();
            DecrementMagazineRpc();
            _playerAmmo.UpdateAmmoUI();
        }
    }

    public void IncrementMagazine()
    {
        IncrementMagazineRpc();
    }

    public void DecrementMagazine()
    {
        DecrementMagazineRpc();
    }

    public void UpdateMagazineText()
    {
        if (_magazineText != null)
        {
            _magazineText.text = MagazineCount.ToString();
        }
    }

    public void SetDefault()
    {
        SetDefaultRpc();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetDefaultRpc()
    {
        MagazineCount = 0;
        UpdateMagazineText();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DecrementMagazineRpc()
    {
        MagazineCount--;
        UpdateMagazineText();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void IncrementMagazineRpc()
    {
        MagazineCount++;
        UpdateMagazineText();
    }
}