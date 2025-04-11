using Fusion;
using UnityEngine;

public class PlayerSetTeam : NetworkBehaviour
{
    private AvatarManager _avatarManager;
    private Vector3 _yellowPosition = new Vector3(-8f, 1f, 17f);
    private Vector3 _redPosition = new Vector3(-290f, 1f, 330f);

    [SerializeField] private TimerManager _timerManager;

    [SerializeField] private GameObject _readyButton;

    private void Start()
    {
        _avatarManager = FindObjectOfType<AvatarManager>();
        _timerManager = FindObjectOfType<TimerManager>();
    }

    public override void Spawned()
    {
        _readyButton.gameObject.SetActive(HasStateAuthority);
    }

    public void TransformToBase()
    {
        _timerManager.IncrementPlayers();
        _avatarManager.DeSpawned(transform.GetComponent<NetworkObject>(), _yellowPosition, _redPosition);
    }
}
