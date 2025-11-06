using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class MouseTracker : MonoBehaviour
{
    public Camera cam;        // assign MainCamera in Inspector
    public bool lockZ = true; // if true, keeps Z constant
    public float fixedZ = 0f; // Z position if locked

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || cam == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Convert mouse position to world point at a fixed distance from camera
        float distance = 3.5f; // adjust as needed
        Vector3 screenPoint = new Vector3(mousePos.x, mousePos.y, distance);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPoint);

        if (lockZ)
            worldPos.z = fixedZ; // lock Z if desired

            // Directly set position to world point (no smoothing)
            transform.position = worldPos;
    }
}
