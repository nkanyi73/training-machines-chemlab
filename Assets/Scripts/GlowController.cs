using UnityEngine;

public class GlowController : MonoBehaviour
{
    public Material metalMaterial;
    public Color glowColor = Color.red;
    public float maxGlowIntensity;
    public float glowSpeed;

    public Color originalColor;
    private float currentGlowIntensity = 0.0f;
    private bool isNearFire = false;

    public GameObject[] liquidMaterials;

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
            metalMaterial.SetColor("_EmissionColor", glowColor * currentGlowIntensity);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            Debug.LogError("Collision Detected");
            isNearFire = true;

            for (int i = 0; i < liquidMaterials.Length; i++)
            {
                if (liquidMaterials[i].gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    liquidMaterials[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }

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