using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BeltSlot : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private XRGrabInteractable _grabInteractable;
    private bool _isHeld = false;
    private Coroutine _storeCoroutine;

    void Start()
    {
        if (_object != null && _grabInteractable != null)
        {
                _grabInteractable.selectEntered.AddListener(OnGrab);
                _grabInteractable.selectExited.AddListener(OnRelease);

            StoreGunImmediately(); 
        }
    }

    void OnDestroy()
    {
        if (_grabInteractable != null)
        {
            _grabInteractable.selectEntered.RemoveListener(OnGrab);
            _grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        _rb.constraints = RigidbodyConstraints.None;
        _isHeld = true;

        if (_storeCoroutine != null)
        {
            StopCoroutine(_storeCoroutine);
            _storeCoroutine = null;
        }     
    }

    void OnRelease(SelectExitEventArgs args)
    {
        _isHeld = false;

        if (_storeCoroutine == null)
        {
            _storeCoroutine = StartCoroutine(DelayedStore());
        }
    }

    IEnumerator DelayedStore()
    {
        yield return new WaitForSeconds(2f);

        if (!_isHeld)
        {
            StoreGunImmediately();
        }

        _storeCoroutine = null;
    }

    void StoreGunImmediately()
    {
        _object.transform.SetParent(transform);
        _object.transform.localPosition = Vector3.zero;
        _object.transform.localRotation = Quaternion.identity;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
