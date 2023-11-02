using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    [HideInInspector] public Vector2 MousePosition;
    [HideInInspector] public Vector2 MouseDirection;
    [HideInInspector] public bool MMBHeld;
    [HideInInspector] public bool LMBHeld;
    [HideInInspector] public float ScrollValue;
    public event System.Action<Vector2> LMBPressed;

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

    public void OnZoom(InputAction.CallbackContext context)
    {
        Vector2 scroll = context.action.ReadValue<Vector2>().normalized;
        ScrollValue = -scroll.y;
    }

    private void Update()
    {
        MousePosition  = Mouse.current.position.ReadValue();
        MouseDirection = Mouse.current.delta.ReadValue();
    }

    private void OnEnable()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else { Instance = this; }

        pInput = gameObject.GetComponent<PlayerInput>();
        pInput.actions.Enable();
    }
}
