using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Fusion;

public class FireBulletOnActivate : NetworkBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletTarget;

    private PlayerAmmo _playerAmmo;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (_playerAmmo != null && _playerAmmo.Ammo > 0)
        { 
            Vector3 direction = (_bulletTarget.position - transform.position).normalized;

            Runner.Spawn(_bullet, _bulletTarget.position, Quaternion.LookRotation(direction), Object.InputAuthority,
                (runner, obj) => {
                    BulletController bullet = obj.GetComponent<BulletController>();
                    bullet.Init(direction * 30f, GetComponent<PlayerHP>());
                }); 

            _playerAmmo.DecrementAmmo();
        }
    }

    public void SetPlayerAmmo(PlayerAmmo newAmmo)
    {
        _playerAmmo = newAmmo;
        _playerAmmo.UpdateAmmoUI();
    }

    public void ClearAmmo()
    {
        _playerAmmo = null;
    }
}
