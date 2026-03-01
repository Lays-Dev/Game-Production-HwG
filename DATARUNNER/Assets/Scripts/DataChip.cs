using UnityEngine;

public class DataChip : MonoBehaviour
{
    public EnemySound enemy;

    [Header("Pickup Effect")]
    public GameObject pickupEffectPrefab;

    public void PickUp()
    {
        Debug.Log("Picked up: Data Chip");

        // Spawn particle effect at chip position
        if (pickupEffectPrefab != null)
        {
            Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);
        }

        if (enemy != null)
        {
            enemy.OnDataChipStolen();
        }

        Destroy(gameObject);
    }
}