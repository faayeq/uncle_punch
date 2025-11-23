using UnityEngine;

public static class BurnFactory
{
    private static Material fireMat;
    private static Texture2D whiteTex;

    private static void EnsureResources()
    {
        // Create white texture ONCE
        if (whiteTex == null)
        {
            whiteTex = new Texture2D(1, 1);
            whiteTex.SetPixel(0, 0, Color.white);
            whiteTex.Apply();
        }

        // Create additive fire material ONCE
        if (fireMat == null)
        {
            fireMat = new Material(Shader.Find("Particles/Additive"));
            fireMat.SetTexture("_MainTex", whiteTex);
        }
    }

    public static GameObject CreateBurnEffectAt(Vector3 position, Transform parent)
    {
        EnsureResources();

        GameObject go = new GameObject("BurnEffect_Procedural");
        go.transform.position = position;
        go.transform.SetParent(parent);

        ParticleSystem ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;

        // Main settings BEFORE Play()
        main.duration = 1f;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.3f, 0.5f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.1f, 0.3f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.25f, 0.4f);
        main.loop = true;
        main.gravityModifier = -0.2f;

        // Emission
        var emission = ps.emission;
        emission.rateOverTime = 40f;

        // Shape (upward cone)
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 5f;
        shape.radius = 0.05f;
        shape.position = Vector3.zero;

        // Color over Lifetime
        var colLife = ps.colorOverLifetime;
        colLife.enabled = true;
        Gradient g = new Gradient();
        g.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(1f, 0.5f, 0f), 0f),   // orange
                  new GradientColorKey(new Color(1f, 1f, 0.2f), 0.5f), // yellow
                  new GradientColorKey(new Color(1f, 0.2f, 0f), 1f)    // reddish
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                  new GradientAlphaKey(0f, 1f)
            }
        );
        colLife.color = g;

        // Renderer
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.material = fireMat;

        ps.Play();
        return go;
    }
}
