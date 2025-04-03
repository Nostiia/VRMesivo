using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty _pinchAnimationAction;
    [SerializeField] private InputActionProperty _gripAnimationAction;

    [SerializeField] private Animator _handAnimator;

    private void Update()
    {
        float triggeredValue = _pinchAnimationAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", triggeredValue);

        float gripValue = _gripAnimationAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", gripValue);
    }
}
