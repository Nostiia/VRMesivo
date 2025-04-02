using TMPro;
using UnityEngine;

public class VRKeyboardManager : MonoBehaviour
{
    private TMP_InputField _inputField;
    private TouchScreenKeyboard _keyboard;

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    public void ShowKeyboard()
    {
        _keyboard = TouchScreenKeyboard.Open(_inputField.text, TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        if (_keyboard != null && _keyboard.active)
        {
            _inputField.text = _keyboard.text;
        }
    }
}
