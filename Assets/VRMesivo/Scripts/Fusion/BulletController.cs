using Fusion;
using UnityEngine;

public class BulletController : NetworkBehaviour
{
    [Networked] private TickTimer Life { get; set; }
    public int Damage { get; private set; } = 10;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;

    private PlayerHP _player;

    public void Init(Vector3 forward, PlayerHP player)
    {
        _player = player;
        Life = TickTimer.CreateFromSeconds(Runner, 10f);
        _rb.velocity = forward;
    }

    public override void FixedUpdateNetwork()
    {
        if (Life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHP enemy = other.gameObject.GetComponent<PlayerHP>();
        if (enemy != null && Object != null)
        {
            enemy.TakeDamage(Damage);
            Runner.Despawn(Object);        
        }
    }
}
