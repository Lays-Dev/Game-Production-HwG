using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light light;
    
    public float minIntensity = .5f;
    public float maxIntensity = 5.0f;
    public float flickerSpeed = 0.14f;

    private void Start()
    {
        light = GetComponent<Light>();

        InvokeRepeating("Flicker", 0f, flickerSpeed);
    }

    private void Flicker()
    {
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        light.intensity = randomIntensity;
    }
}
