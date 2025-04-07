using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunMagazineSocket : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private FireBulletOnActivate fireScript;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnMagInserted);
        socket.selectExited.AddListener(OnMagRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnMagInserted);
        socket.selectExited.RemoveListener(OnMagRemoved);
    }

    private void OnMagInserted(SelectEnterEventArgs args)
    {
        PlayerAmmo ammo = args.interactableObject.transform.GetComponent<PlayerAmmo>();
        if (ammo != null)
        {
            fireScript.SetPlayerAmmo(ammo);
            Debug.Log("Magazine inserted and ammo linked.");
        }
        else
        {
            Debug.LogWarning("Inserted object has no PlayerAmmo script!");
        }
    }

    private void OnMagRemoved(SelectExitEventArgs args)
    {
        fireScript.ClearAmmo();
        Debug.Log("Magazine removed. Ammo reference cleared.");
    }
}
