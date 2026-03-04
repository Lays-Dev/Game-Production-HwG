// Finish enemy with animations, updated models, and texture
// Make the enemy into a prefab
// Drag the prefab into this script in the inspector 
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Drag your prefab here in the inspector
    public GameObject objectToSpawn; 

    void Start()
    {
        // Spawns at the position and rotation of the object holding this script
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
