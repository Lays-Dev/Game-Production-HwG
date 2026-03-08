using UnityEngine;
using UnityEngine.InputSystem;

public class DataChip : MonoBehaviour
{
    public static bool playerHasDataChip = false;

    private bool playerInRange = false;

    // UI functionality
    public UIText uiText;

    [Header("VFX")]
    public GameObject pickupVFX;
    public Transform vfxSpawnPoint; // where the VFX will appear

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
        playerHasDataChip = true;

        // Spawn VFX at the custom location
        if (pickupVFX != null && vfxSpawnPoint != null)
        {
            Instantiate(pickupVFX, vfxSpawnPoint.position, vfxSpawnPoint.rotation);
        }

        uiText.UpdateObjectiveText("Objective - Escape");

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
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