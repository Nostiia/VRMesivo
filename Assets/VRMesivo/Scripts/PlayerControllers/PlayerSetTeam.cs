using Fusion;
using UnityEngine;

public class PlayerSetTeam : NetworkBehaviour
{
    private AvatarManager _avatarManager;
    [SerializeField] private Transform _yellowPosition;
    [SerializeField] private Transform _redPosition;

    [SerializeField] private GameObject _yellowTeam;
    [SerializeField] private GameObject _redTeam;
    [SerializeField] private TimerManager _timerManager;

    [SerializeField] private GameObject _readyButton;

    private void Start()
    {
        _avatarManager = FindObjectOfType<AvatarManager>();
        _timerManager = FindObjectOfType<TimerManager>();
        _yellowTeam = GameObject.Find("YellowTeam");
        _yellowPosition = _yellowTeam.transform;

        _redTeam = GameObject.Find("RedTeam");
        _redPosition = _redTeam.transform;
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
