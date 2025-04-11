using UnityEngine;
using UnityEngine.InputSystem;

public class VRHUDController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private InputAction _xButtonAction;
    private bool _isVisible = false;

    [SerializeField] private InputActionReference _xButton;


    private void Awake()
    {
        _xButtonAction = _xButton;

        _xButtonAction.Enable();
        _xButtonAction.performed += ToggleCanvas;
    }

    private void ToggleCanvas(InputAction.CallbackContext ctx)
    {
        _isVisible = !_isVisible;
        _canvas.SetActive(_isVisible);
    }

    private void OnDestroy()
    {
        _xButtonAction.performed -= ToggleCanvas;
    }
}