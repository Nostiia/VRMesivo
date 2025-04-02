using Fusion;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private BulletController _prefabBullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerHP _playerHP;
    [SerializeField] private PlayerAmmo _playerAmmo;
    [Networked] private TickTimer _delay { get; set; }

    private GameController _gameController;

    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
    }

    private void Update()
    {
        if (!Object.HasStateAuthority) return;
        if (Input.GetKeyDown(KeyCode.B) && (_delay.ExpiredOrNotRunning(Runner)) 
            && _gameController.GameContinue && _playerAmmo.Ammo > 0 )
        {
            Shoot();
            _playerAmmo.DecrementAmmo();
        }
    }

    private void Shoot()
    {
        if (!Object.HasStateAuthority) return;

        _delay = TickTimer.CreateFromSeconds(Runner, 0.5f); 

        Vector3 targetPoint = GetMouseWorldPosition();

        Vector3 direction = (targetPoint - _shootPoint.position).normalized;

        Runner.Spawn(_prefabBullet, _shootPoint.position, Quaternion.LookRotation(direction), Object.InputAuthority,
            (runner, obj) => {
                BulletController bullet = obj.GetComponent<BulletController>();
                bullet.Init(direction * 10f, GetComponent<PlayerHP>());
            });
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return _shootPoint.position + _shootPoint.forward * 10f;
    }
}
