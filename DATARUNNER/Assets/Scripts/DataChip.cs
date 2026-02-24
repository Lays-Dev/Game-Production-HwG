using UnityEngine;

public class DataChip : MonoBehaviour
{
    public void PickUp()
    {
        Debug.Log("Picked up: Data Chip");
        Destroy(gameObject);
    }
}