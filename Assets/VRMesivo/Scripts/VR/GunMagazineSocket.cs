using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunMagazineSocket : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor _socket;
    [SerializeField] private FireBulletOnActivate _fireScript;

    private void OnEnable()
    {
        _socket.selectEntered.AddListener(OnMagInserted);
        _socket.selectExited.AddListener(OnMagRemoved);
    }

    private void OnDisable()
    {
        _socket.selectEntered.RemoveListener(OnMagInserted);
        _socket.selectExited.RemoveListener(OnMagRemoved);
    }

    private void OnMagInserted(SelectEnterEventArgs args)
    {
        PlayerAmmo ammo = args.interactableObject.transform.GetComponent<PlayerAmmo>();
        if (ammo != null)
        {
            _fireScript.SetPlayerAmmo(ammo);
        }
        else
        {
            Debug.LogWarning("Inserted object has no PlayerAmmo script!");
        }
    }

    private void OnMagRemoved(SelectExitEventArgs args)
    {
        _fireScript.ClearAmmo();
    }
}
