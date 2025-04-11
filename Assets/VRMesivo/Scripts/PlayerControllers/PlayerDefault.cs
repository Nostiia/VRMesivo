using UnityEngine;
using Fusion;

public class PlayerDefault : NetworkBehaviour
{
    private NetworkObject _currentNetworkObject;

    private GameObject _playerPrefab;
    private Vector3 _teamPosition;

    public void SetValues(GameObject _playerPref, Vector3 _teamPos)
    {
        _playerPrefab = _playerPref;
        _teamPosition = _teamPos;
    }

    public void SetDefault()
    {
        RPC_RequestSpawnToStart();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_RequestSpawnToStart()
    {
        NetworkObject player = transform.GetComponent<NetworkObject>();

        _currentNetworkObject = Runner.Spawn(
            _playerPrefab,
            _teamPosition,
            Quaternion.identity,
            Object.InputAuthority
        );

        if (player != null)
        {
            Runner.Despawn(player);
        }
    }
}

