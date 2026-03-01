using UnityEngine;

public class DataChip : MonoBehaviour
{
    public EnemySound enemy;

    public void PickUp()
    {
        Debug.Log("Picked up: Data Chip");

        if (enemy != null)
        {
            enemy.OnDataChipStolen();
        }

        Destroy(gameObject);
    }
}