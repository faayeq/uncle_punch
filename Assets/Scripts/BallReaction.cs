using UnityEngine;

public class BallReaction : MonoBehaviour
{
    public float punchForce = 10f; // increase for stronger push

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we collided with the Arm
        if (collision.gameObject.CompareTag("Arm"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Get direction from arm to ball
                Vector3 dir = (transform.position - collision.transform.position).normalized;

                // Apply a quick impulse
                rb.AddForce(dir * punchForce, ForceMode.Impulse);
            }
        }
    }
}
