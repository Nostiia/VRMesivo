using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class MagBeltSlot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private Rigidbody _rb;

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;
    private Coroutine storeCoroutine;

    void Start()
    {
        if (gun != null)
        {
            grabInteractable = gun.GetComponent<XRGrabInteractable>();

            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.AddListener(OnGrab);
                grabInteractable.selectExited.AddListener(OnRelease);
            }

            StoreMagImmediately(); // Snap to belt at start
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        _rb.constraints = RigidbodyConstraints.None;
        isHeld = true;

        // Cancel storing if we grab it during delay
        if (storeCoroutine != null)
        {
            StopCoroutine(storeCoroutine);
            storeCoroutine = null;
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;

        // Start delayed store
        if (storeCoroutine == null)
        {
            storeCoroutine = StartCoroutine(DelayedStore());
        }
    }

    IEnumerator DelayedStore()
    {
        yield return new WaitForSeconds(2f);

        // Only store if still not held
        if (!isHeld)
        {
            StoreMagImmediately();
        }

        storeCoroutine = null;
    }

    void StoreMagImmediately()
    {
        gun.transform.SetParent(transform);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
