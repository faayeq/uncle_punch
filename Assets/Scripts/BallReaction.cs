using UnityEngine;

public class BallReaction : MonoBehaviour
{
    public float punchForce = 10f;
    public GameObject burnEffectPrefab;
    public float burnDuration = 2f;
    public GameObject ashExplosionPrefab;

    private Renderer ballRenderer;
    private Color originalColor;

    private void Start()
    {
        ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null)
            originalColor = ballRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arm"))
        {
            // ❌ No movement anymore
            // Rigidbody rb = GetComponent<Rigidbody>();
            // if (rb != null)
            // {
            //     Vector3 dir = (transform.position - other.transform.position).normalized;
            //     rb.AddForce(dir * punchForce, ForceMode.Impulse);
            // }

            // 🖤 Turn ball black
            if (ballRenderer != null)
                ballRenderer.material.color = Color.black;

            // --- 🔥 FIX: proper hit point even with triggers ---
            Collider ballCol = GetComponent<Collider>();
            Collider armCol = other.GetComponent<Collider>();

            Vector3 hitPoint = ballCol.ClosestPoint(armCol.transform.position);

            GameObject burn = Instantiate(burnEffectPrefab, hitPoint, Quaternion.identity);
            burn.transform.SetParent(transform);
            // ---------------------------------------------------

            // ⚡ Spawn ash when fire ends
            Invoke(nameof(SpawnAshAndDestroy), burnDuration - 0.05f);

            Destroy(burn, burnDuration);
        }
    }

    private void SpawnAshAndDestroy()
    {
        if (ashExplosionPrefab != null)
            Instantiate(ashExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
