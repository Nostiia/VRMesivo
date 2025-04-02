using UnityEngine;
using Fusion;

public class AvatarManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    private NetworkObject _currentNetworkObject;
    [Networked] public int IndexOfChoosen { get; private set; } = -1;

    private bool isChangingAvatar = false;
    private Transform _teamPosition;

    public void DeSpawned(NetworkObject player, Transform yellowTeam, Transform redTeam)
    {
        _currentNetworkObject = player;
        if (!isChangingAvatar)
        {
            isChangingAvatar = true;
            ChooseAvatar();
            ChangeAvatar(IndexOfChoosen, player, yellowTeam, redTeam);         
        }
    }

    public void ChooseAvatar()
    {
        Debug.Log(IndexOfChoosen);
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

    public void ChangeAvatar(int newIndex, NetworkObject player, Transform yellowTeam, Transform redTeam)
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
            _teamPosition.position,
            Quaternion.identity,
            Object.InputAuthority
        );

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