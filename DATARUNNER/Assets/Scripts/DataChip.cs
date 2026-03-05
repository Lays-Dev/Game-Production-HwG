using UnityEngine;
using UnityEngine.InputSystem;

public class DataChip : MonoBehaviour
{
    public static bool playerHasDataChip = false;

    private bool playerInRange = false;
    // UI functionality
    public UIText uiText;

void Update()
{
    if (playerInRange)
    {
        //Debug.Log("Player in range");

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E detected!");
            PickUp();
        }
    }
}

    void PickUp()
    {
        playerHasDataChip = true;

        //Debug.Log("Picked up: Data Chip");
        Destroy(gameObject);
        uiText.UpdateObjectiveText("Objective - Escape");

        //FindObjectOfType<UIText>().UpdateObjectiveText("Objective: Escape");

    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log("Player entered chip range");
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