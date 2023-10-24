using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private const int MIN_YAW        = 15; 
    private const int MAX_YAW        = 45;
    private const float ZOOM_STEP    = 10;
    private const float MAX_ZOOM_OUT = 50;
    private const float MAX_ZOOM_IN  = 15;
    
    private float xRot = 0;
    private float yRot = 0;

    [SerializeField] private Camera cam;

    private void moveCamera()
    {
        if (!InputHandler.Instance.MMBHeld) { return; }

        float xIncrement = -InputHandler.Instance.MouseDirection.y * GameSettings.MouseSensitivity * Time.deltaTime;
        xRot += xIncrement;
        xRot = Mathf.Clamp(xRot, MIN_YAW, MAX_YAW);
        float yIncrement = InputHandler.Instance.MouseDirection.x * GameSettings.MouseSensitivity * Time.deltaTime;
        yRot += yIncrement;

        transform.rotation = Quaternion.Euler(xRot, yRot, 0); 
    }

    private void zoomCamera()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance >= MAX_ZOOM_OUT && InputHandler.Instance.ScrollValue >= 0) { return; }
        else if (distance <= MAX_ZOOM_IN && InputHandler.Instance.ScrollValue <= 0) { return; }      

        Vector3 dir    = (cam.transform.position - transform.position).normalized;
        Vector3 step   = dir * InputHandler.Instance.ScrollValue * ZOOM_STEP;

        cam.transform.position += step; 
    }

    private void Update()
    {
        moveCamera();
        zoomCamera(); 
    }
}
