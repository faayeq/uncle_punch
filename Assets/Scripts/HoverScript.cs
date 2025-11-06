using UnityEngine;

public class HoverScript : MonoBehaviour
{
    public Transform target;        // Assign Reference sphere here
    public float smoothSpeed = 10f; // optional smoothing

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;

        if (direction.sqrMagnitude < 0.0001f) return;

        // Rotate cylinder (Y-up) toward the target
        Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, direction);

        // Smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
    }
}
