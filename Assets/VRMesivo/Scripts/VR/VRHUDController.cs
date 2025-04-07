using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class VRHUDController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text timerText;
    public TMP_Text ammoText;
    public TMP_Text hpText;

    private InputAction xButtonAction;
    private bool isVisible = false;

    public InputActionReference xButton;


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