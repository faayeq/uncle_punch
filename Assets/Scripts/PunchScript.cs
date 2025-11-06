using UnityEngine;
using UnityEngine.InputSystem;

public class PunchScript : MonoBehaviour
{
    public float punchScaleMultiplier = 2f; // how much to extend (2 = double the length)
    public float punchDuration = 0.2f;      // total time of punch
    private Vector3 originalScale;
    private bool isPunching = false;
    private float timer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // On left mouse click, start punch
        if (Mouse.current.leftButton.wasPressedThisFrame && !isPunching)
        {
            isPunching = true;
            timer = 0f;
        }

        if (isPunching)
        {
            timer += Time.deltaTime;

            // First half: extend
            if (timer < punchDuration / 2f)
            {
                float t = timer / (punchDuration / 2f);
                transform.localScale = new Vector3(
                    originalScale.x,
                    Mathf.Lerp(originalScale.y, originalScale.y * punchScaleMultiplier, t),
                                                   originalScale.z
                );
            }
            // Second half: retract
            else if (timer < punchDuration)
            {
                float t = (timer - punchDuration / 2f) / (punchDuration / 2f);
                transform.localScale = new Vector3(
                    originalScale.x,
                    Mathf.Lerp(originalScale.y * punchScaleMultiplier, originalScale.y, t),
                                                   originalScale.z
                );
            }
            // Done
            else
            {
                transform.localScale = originalScale;
                isPunching = false;
                timer = 0f;
            }
        }
    }
}
