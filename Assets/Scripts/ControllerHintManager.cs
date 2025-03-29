using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHintManager : MonoBehaviour
{
    public Material targetMaterial; // Reference to the material to change
    public Color emissionColor = Color.white; // Color of the emission
    public float minIntensity; // Minimum intensity of the glow
    public float maxIntensity; // Maximum intensity of the glow
    public float lerpDuration = 1.0f; // Duration for one cycle of lerp
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMaterialEmission(minIntensity);
    }

    // Update is called once per frame
    void Update()
    {
        // Lerp between min and max intensity over time
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / lerpDuration, 1); // PingPong to oscillate
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        UpdateMaterialEmission(intensity);
    }

    void UpdateMaterialEmission(float intensity)
    {
        targetMaterial.SetColor("_EmissionColor", emissionColor * intensity);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), targetMaterial.GetColor("_EmissionColor")); // Update GI
    }
}
