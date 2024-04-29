using UnityEngine;

public class GlowController : MonoBehaviour
{
    public Material metalMaterial;
    public Color glowColor = Color.red;
    public float maxGlowIntensity = 2.0f;
    public float glowSpeed = 1.0f;

    public Color originalColor;
    private float currentGlowIntensity = 0.0f;
    private bool isNearFire = false;

    void Start()
    {
        // Store original material color
        originalColor = metalMaterial.GetColor("_EmissionColor");
    }

    void Update()
    {
        if (isNearFire)
        {
            // Increase glow intensity
            currentGlowIntensity = Mathf.Lerp(currentGlowIntensity, maxGlowIntensity, Time.deltaTime * glowSpeed);
            metalMaterial.SetColor("_EmissionColor", glowColor * currentGlowIntensity);
        }
        else
        {
            // Decrease glow intensity
            currentGlowIntensity = Mathf.Lerp(currentGlowIntensity, 0.0f, Time.deltaTime * glowSpeed);
            metalMaterial.SetColor("_EmissionColor", originalColor * currentGlowIntensity);
        }

        Debug.Log(currentGlowIntensity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            Debug.LogError("Collision Detected");
            isNearFire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            isNearFire = false;
        }
    }
}