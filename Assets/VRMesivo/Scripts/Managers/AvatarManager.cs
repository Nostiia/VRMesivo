using UnityEngine;
using Fusion;

public class AvatarManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    private NetworkObject _currentNetworkObject;
    [Networked] public int IndexOfChoosen { get; private set; } = -1;

    private bool _isChangingAvatar = false;
    private Vector3 _teamPosition;

    public void DeSpawned(NetworkObject player, Vector3 yellowTeam, Vector3 redTeam)
    {
        _currentNetworkObject = player;
        if (!_isChangingAvatar)
        {
            _isChangingAvatar = true;
            ChooseAvatar();
            ChangeAvatar(IndexOfChoosen, player, yellowTeam, redTeam);         
        }
    }

    public void ChooseAvatar()
    {
        if (IndexOfChoosen == -1)
        {
            IndexOfChoosen = Random.Range(0, _playerPrefabs.Length);
        }
        else
        {
            IndexOfChoosen = (IndexOfChoosen == 0) ? 1 : 0;
        }
        RPC_SetChosenAvatar(IndexOfChoosen);
    }

    public void ChangeAvatar(int newIndex, NetworkObject player, Vector3 yellowTeam, Vector3 redTeam)
    {        
        switch (newIndex)
        {
            case 0:
                _teamPosition = yellowTeam;
                break;
            case 1:
                _teamPosition = redTeam;
                break;
        }
        if (player != null)
        {
            Runner.Despawn(player);
        }

        _currentNetworkObject = Runner.Spawn(
            _playerPrefabs[newIndex],
            _teamPosition,
            Quaternion.identity,
            Object.InputAuthority
        );
        Debug.Log($"Spawning player at {_teamPosition}");
        Debug.Log(_currentNetworkObject.transform.position);

        if (_currentNetworkObject.TryGetComponent(out PlayerDefault playerScript))
        {
            playerScript.SetValues(_playerPrefabs[newIndex], _teamPosition);
        }

        IndexOfChoosen = newIndex;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetChosenAvatar(int chosenIndex)
    {
        IndexOfChoosen = chosenIndex;
    }
}