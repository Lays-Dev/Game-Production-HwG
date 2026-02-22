// If item is static
// > No Nav-Mesh Obstacle component
// > If it does have Nav-Mesh then distroy it on pickup
// Data Chip must have a collider with Is Trigger checked
// Data Chip should have a rigidbody kinematic checked
// Player must have Character Controller
// Player must have Player tag
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Variables
    // Enemy goes here
    public EnemySound Enemy;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Alert the enemy that item was picked up
            Enemy.AlertToPlayerLocation();
            // Pick up the item
            Debug.Log("Item picked up! Enemy is coming");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}