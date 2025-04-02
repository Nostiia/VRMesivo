using Fusion;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] private Camera _playerCamera;
    public override void Spawned()
    {
        _playerCamera.gameObject.SetActive(HasStateAuthority);
    }
}
