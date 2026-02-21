// If item is static
// > No Nav-Mesh Obstacle component
// > If it does have Nav-Mesh then distroy it on pickup
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Assign the enemy here
    public EnemySound enemyScript; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Alert the enemy
            enemyScript.AlertToPlayerLocation();

            // Pick up the item
            Debug.Log("Item picked up! Enemy is coming...");
            Destroy(gameObject); 
        }
    }
}