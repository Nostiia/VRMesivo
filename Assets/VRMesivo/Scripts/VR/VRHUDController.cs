using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class VRHUDController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private InputAction xButtonAction;
    private bool isVisible = false;

    [SerializeField] private InputActionReference xButton;


    void Awake()
    {
        xButtonAction = xButton;

        xButtonAction.Enable();
        xButtonAction.performed += ToggleCanvas;
    }

    private void ToggleCanvas(InputAction.CallbackContext ctx)
    {
        isVisible = !isVisible;
        canvas.SetActive(isVisible);
    }

    private void OnDestroy()
    {
        xButtonAction.performed -= ToggleCanvas;
    }
}