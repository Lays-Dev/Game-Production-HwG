// Attach this to an empty game object
// Directive
using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    // Variable declarations
    [Header("Pyramid Settings")]
    // Range restricts size
    [Range(3, 10)] public int pyramidBaseSize = 8;

    [Header("Forest Settings")]
    public int treeCount = 50;
    public float forestAreaSize = 20f;
    public float minTreeDistance = 2f;

    [Header("Celestial Settings")]
    public float rotationSpeed = 12f;
    // sun is the Celestial Game Object
    private GameObject sun;

// Runs when you click play
    void Start()
    {
        // Remove the light because its making the day and night cycle less noticeable
        CleanupDefaultScene();
        GenerateScene();
    }

    void CleanupDefaultScene()
    {
        // Finds and destroys the default Directional Light that comes with a new scene
        Light[] allLights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
        foreach (Light l in allLights) { Destroy(l.gameObject); }
    }
    void Update()
    {
        // Future Idea: Add shadows that change when the sun moves
        // Don't have time to implement this rn
        if (sun != null)
        {
            // Rotate around the center of the world
            sun.transform.RotateAround(Vector3.zero, Vector3.right, rotationSpeed * Time.deltaTime);
            sun.transform.LookAt(Vector3.zero);
            
            // Switch from day to night based on height
            Light light = sun.GetComponent<Light>();
            light.intensity = sun.transform.position.y > 0 ? 1.0f : 0.05f;
        }
    }

    void GenerateScene()
    {
        // Create hierarchy parents
        GameObject groundParent = new GameObject("Ground");
        GameObject pyramidParent = new GameObject("Pyramid");
        GameObject forestParent = new GameObject("Forest");

        // Create plane for ground
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.parent = groundParent.transform;
        floor.transform.localScale = new Vector3(5, 1, 5);

        // Create pyramid
        for (int level = 0; level < pyramidBaseSize; level++)
        {
            int currentLevelSize = pyramidBaseSize - level;
            // Each level gets a random color
            Color levelColor = new Color(Random.value, Random.value, Random.value);
            
            for (int x = 0; x < currentLevelSize; x++)
            {
                for (int z = 0; z < currentLevelSize; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = pyramidParent.transform;
                    
                    // Offset each level to center it
                    float offset = level * 0.5f;
                    cube.transform.position = new Vector3(x + offset, level, z + offset);
                    
                    cube.GetComponent<Renderer>().material.color = levelColor;
                }
            }
        }

        // Forest generation
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randomPos = new Vector3
            (
                Random.Range(-forestAreaSize, forestAreaSize), 
                0.5f, 
                Random.Range(-forestAreaSize, forestAreaSize)
            );
            
            // Check distance from trees
            Collider[] hitColliders = Physics.OverlapSphere(randomPos, minTreeDistance);
            if (hitColliders.Length <= 1)
            {
                GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                trunk.transform.parent = forestParent.transform;
                trunk.transform.position = randomPos;
                trunk.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                trunk.GetComponent<Renderer>().material.color = new Color(0.4f, 0.2f, 0.1f);
            }
        }

        // Create sun
        sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.name = "CelestialBody";
        sun.transform.position = new Vector3(0, 20, -20);
        sun.AddComponent<Light>().type = LightType.Directional;
    }
}