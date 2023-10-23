using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [HideInInspector] public Vector2 MousePosition;
    [HideInInspector] public Vector2 MouseDirection;
    [HideInInspector] public bool MMBHeld;
    [HideInInspector] public bool LMBHeld; 
    public event Vector2Delegate LMBPressed;

    private PlayerInput pInput;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (LMBPressed != null) { LMBPressed.Invoke(Mouse.current.position.ReadValue()); }
        }
        if (context.started) { LMBHeld = true; }
        if (context.canceled) { LMBHeld = false; }
    }

    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        if (context.started) { MMBHeld = true; }
        if (context.canceled) { MMBHeld = false; }
    }

    private void Update()
    {
        MousePosition  = Mouse.current.position.ReadValue();
        MouseDirection = Mouse.current.delta.ReadValue();
    }

    private void OnEnable()
    {
        pInput = gameObject.GetComponent<PlayerInput>();
        pInput.actions.Enable();
    }

    public delegate void Vector2Delegate(Vector2 position);
    public delegate void EventDelegate(); 
}
