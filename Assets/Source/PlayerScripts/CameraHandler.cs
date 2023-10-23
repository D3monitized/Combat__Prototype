using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private int MIN_YAW = 15; 
    private int MAX_YAW = 45;
    
    private float xRot = 0;
    private float yRot = 0;

    [SerializeField] private Camera cam;
    private InputHandler inputHandler;

    private void moveCamera()
    {
        if (!inputHandler.MMBHeld) { return; }

        float xIncrement = -inputHandler.MouseDirection.y * GameSettings.MouseSensitivity * Time.deltaTime;
        xRot += xIncrement;
        xRot = Mathf.Clamp(xRot, MIN_YAW, MAX_YAW);
        float yIncrement = inputHandler.MouseDirection.x * GameSettings.MouseSensitivity * Time.deltaTime;
        yRot += yIncrement;

        transform.rotation = Quaternion.Euler(xRot, yRot, 0); 
    }

    private void Update()
    {
        moveCamera();
    }

    private void OnEnable()
    {
        inputHandler = GameObject.Find("InputHandler")?.GetComponent<InputHandler>();

        if (!inputHandler)
        {
            Debug.LogWarning("InputHandler not found" + " - CameraHandler");
        }
    }
}
