using UnityEngine;
using Fusion;

public class PlayerDefault : NetworkBehaviour
{
    private NetworkObject _currentNetworkObject;

    private GameObject _playerPrefab;
    private Transform _teamPosition;

    private MagazineManager _magazineManager;

    public void SetValues(GameObject _playerPref, Transform _teamPos)
    {
        _playerPrefab = _playerPref;
        _teamPosition = _teamPos;
    }

    public void SetDefault()
    {
        _magazineManager = FindObjectOfType<MagazineManager>();
        RPC_RequestSpawnToStart();
        _magazineManager.DespawnedAndSpawned();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_RequestSpawnToStart()
    {
        NetworkObject player = transform.GetComponent<NetworkObject>();

        _currentNetworkObject = Runner.Spawn(
            _playerPrefab,
            _teamPosition.position,
            Quaternion.identity,
            Object.InputAuthority
        );

        if (player != null)
        {
            Runner.Despawn(player);
        }
    }
}

